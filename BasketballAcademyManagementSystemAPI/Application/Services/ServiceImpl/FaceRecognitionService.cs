
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Drawing;
using SixLabors.Fonts;
using Rectangle = SixLabors.ImageSharp.Rectangle;
using Font = SixLabors.Fonts.Font;
using Color = SixLabors.ImageSharp.Color;
using Path = System.IO.Path;
using Image = SixLabors.ImageSharp.Image;
using PointF = SixLabors.ImageSharp.PointF;
using RectangleF = SixLabors.ImageSharp.RectangleF;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class FaceRecognitionService : IFaceRecognitionService
    {
        private readonly IAmazonRekognition _rekognition;
        private readonly IUserFaceRepository _userFaceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IManagerRepository _managerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private const string CollectionId = "bams";
        private readonly IFileUploadService _fileUploadService;

        public FaceRecognitionService(IAmazonRekognition rekognition
            , IUserFaceRepository userFaceRepository
            , IHttpContextAccessor httpContextAccessor
            , IManagerRepository managerRepository
            , IUserRepository userRepository
            , IConfiguration configuration
            , IFileUploadService fileUploadService)
        {
            _rekognition = rekognition;
            _userFaceRepository = userFaceRepository;
            _httpContextAccessor = httpContextAccessor;
            _managerRepository = managerRepository;
            _userRepository = userRepository;
            _configuration = configuration;
            _fileUploadService = fileUploadService;
        }

        public async Task EnsureCollectionExists(string collectionId = "bams")
        {
            var list = await _rekognition.ListCollectionsAsync(new ListCollectionsRequest());
            if (!list.CollectionIds.Contains(collectionId))
            {
                await _rekognition.CreateCollectionAsync(new CreateCollectionRequest
                {
                    CollectionId = collectionId
                });
            }
        }

        private ApiResponseModel<object> ValidateImage(IFormFile image)
        {
            var response = new ApiResponseModel<object>
            {
                Data = new RegisterFaceResponse(),
                Message = FaceRecognitionMessage.Error.RegisterUserFaceFailed,
                Errors = new List<string>(),
            };

            if (image == null || image.Length == 0)
            {
                response.Errors.Add(FaceRecognitionMessage.Error.ImageFileNotFound);
                return response;
            }


            var extension = System.IO.Path.GetExtension(image.FileName).ToLowerInvariant();
            if (!FaceRecognitionConstant.Setting.ValidExtensions.Contains(extension))
            {
                response.Errors.Add(FaceRecognitionMessage.Error.ImageFileInValidFormat);
                return response;
            }

            // Load image max size from config
            var faceRecognitionSettingsSection = _configuration.GetSection(FaceRecognitionConstant.Setting.FaceRecognitionSettingSection);
            var imageMaxSize = Double.Parse(faceRecognitionSettingsSection[FaceRecognitionConstant.Setting.MaxImageSize] ?? "5");
            if (image.Length > imageMaxSize * 1024 * 1024)
            {
                response.Errors.Add(FaceRecognitionMessage.Error.ImageFileExceedMaxSize(imageMaxSize));
                return response;
            }

            return response;
        }

        private async Task RemoveRegisteredFace(string imageUrl, List<string> registeredFaceIds)
        {
            // Xoá file đã up lên cloud nếu đăng ký face thất bại
            _fileUploadService.DeleteFile(imageUrl);

            // Remove registered face on aws
            if (registeredFaceIds != null && registeredFaceIds.Count > 0)
            {
                var deleteRequest = new DeleteFacesRequest
                {
                    CollectionId = CollectionId,
                    FaceIds = registeredFaceIds
                };
                await _rekognition.DeleteFacesAsync(deleteRequest);
            }
        }

        #region register face
        public async Task<ApiResponseModel<RegisterFaceResponse>> RegisterFacesForAPlayerAsync(string userId, IFormFile image)
        {
            var response = new ApiResponseModel<RegisterFaceResponse>
            {
                Data = new RegisterFaceResponse { RegisteredFaces = new List<UserFaceDto> () },
                Message = FaceRecognitionMessage.Error.DetectFaceFailed,
                Errors = new List<string>()
            };

            // Kiểm tra quyền manager
            var manager = await GetCurrentManagerAsync(response);
            if (manager == null) return response;

            // Kiểm tra user và cùng team
            var user = await _userRepository.GetUserWithRoleByIdAsync(userId);
            if (!ValidateUserAndTeam(user, manager, response)) return response;

            // Kiểm tra định dạng ảnh
            var validateResponse = ValidateImage(image);
            if (validateResponse.Errors?.Any() == true)
            {
                response.Errors.AddRange(validateResponse.Errors);
                return response;
            }

            var fileName = Guid.NewGuid().ToString();
            var imageUrl = await _fileUploadService.UploadFileAsync(image, "face-recognition", fileName);

            if (string.IsNullOrWhiteSpace(imageUrl) || !imageUrl.Contains(fileName))
            {
                response.Errors.Add(FaceRecognitionMessage.Error.ThisImageAlreadyUsed);
                return response;
            }

            var userFaceIds = await RegisterFaceAsync(userId, image, fileName);
            if (userFaceIds.Count != 1)
            {
                response.Errors.Add(userFaceIds.Count < 1
                    ? FaceRecognitionMessage.Error.ThereAreNoFaceDetectedInTheImage
                    : FaceRecognitionMessage.Error.OnlyOneFaceInImageRequired);

                await RemoveRegisteredFace(imageUrl, userFaceIds);
                return response;
            }

            await SaveRegisteredFace(userId, imageUrl, userFaceIds[0], response.Data);

            if (response.Data.RegisteredFaces.Any())
            {
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = FaceRecognitionMessage.Success.RegisterUserFaceSuccessfully;
            }

            return response;
        }

        private async Task<Manager?> GetCurrentManagerAsync(ApiResponseModel<RegisterFaceResponse> response)
        {
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var manager = await _managerRepository.GetManagerByUserIdAsync(currentUserId);

            if (manager == null)
            {
                response.Errors?.Add(FaceRecognitionMessage.Error.ManagerNotFound);
            }

            return manager;
        }

        private bool ValidateUserAndTeam(Models.User? user, Manager manager, ApiResponseModel<RegisterFaceResponse> response)
        {
            if (user == null)
            {
                response.Errors?.Add(FaceRecognitionMessage.Error.UserNotFound);
                return false;
            }

            bool isSameTeam = user.RoleCode switch
            {
                RoleCodeConstant.PlayerCode => user.Player?.TeamId == manager.TeamId,
                RoleCodeConstant.CoachCode => user.Coach?.TeamId == manager.TeamId,
                _ => false
            };

            if (!isSameTeam || !user.IsEnable)
            {
                response.Errors?.Add(FaceRecognitionMessage.Error.YouCanOnlyRegisterFaceForYourTeamMember);
                return false;
            }

            return true;
        }

        private async Task SaveRegisteredFace(string userId, string imageUrl, string faceId, RegisterFaceResponse data)
        {
            var userFace = new UserFace
            {
                RegisteredFaceId = faceId,
                ImageUrl = imageUrl,
                RegisteredAt = DateTime.Now,
                UserId = userId
            };

            data.RegisteredFaces.Add(new UserFaceDto
            {
                UserId = userId,
                ImageUrl = imageUrl,
                RegisteredAt = userFace.RegisteredAt,
                RegisteredFaceId = faceId
            });

            await _userFaceRepository.RegisterFacesAsync(userFace);
        }

        public async Task<List<string>> RegisterFaceAsync(string userId, IFormFile image, string imageFileName)
        {
            // Ensure collection exists before registering faces
            await EnsureCollectionExists(CollectionId);

            // convert IFormFile to MemoryStream
            var memoryStream = await ToMemoryStreamAsync(image);
            await image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            // Create request to register face
            var request = new IndexFacesRequest
            {
                CollectionId = CollectionId,
                DetectionAttributes = new List<string>(),
                ExternalImageId = imageFileName,
                Image = new Amazon.Rekognition.Model.Image { Bytes = memoryStream }
            };

            // Register face with AWS Rekognition
            var result = await _rekognition.IndexFacesAsync(request);
            return result.FaceRecords.Select(f => f.Face.FaceId).ToList();
        }
        #endregion

        

        #region detect faces
        public async Task<ApiResponseModel<DetectFaceResponse>> DetectFacesInGroupAsync(IFormFile image)
        {
            var response = new ApiResponseModel<DetectFaceResponse>
            {
                Data = new DetectFaceResponse { DetectedFaces = new List<DetectedFaceInformation>() },
                Message = FaceRecognitionMessage.Error.DetectFaceFailed,
                Errors = new List<string>()
            };

            try
            {
                var validateResponse = ValidateImage(image);
                if (validateResponse.Errors?.Any() == true)
                {
                    response.Errors.AddRange(validateResponse.Errors);
                    return response;
                }

                using var imageStream = image.OpenReadStream();
                using var imageSharp = await Image.LoadAsync<Rgba32>(imageStream);
                var awsBytes = await CloneToMemoryStreamAsync(imageStream);

                var detectResult = await DetectFacesFromAWSAsync(awsBytes);
                var font = LoadFont();
                var drawOptions = GetDrawingOptions();

                var faceInfos = new List<DetectedFaceInformation>();

                foreach (var faceDetail in detectResult.FaceDetails)
                {
                    var faceInfo = await ProcessFaceDetectionAsync(imageSharp, faceDetail, font, drawOptions);
                    if (faceInfo == null)
                    {
                        continue;
                    }
                    else
                    {
                        faceInfos.Add(faceInfo);

                    }
                }

                response.Data.DetectedFaces = faceInfos;
                response.Data.OriginalImageUrl = $"data:image/jpeg;base64,{Convert.ToBase64String(awsBytes.ToArray())}";
                response.Data.ProcessedImageUrl = await ConvertImageToBase64(imageSharp);

                if (faceInfos.Count > 0)
                {
                    response.Status = ApiResponseStatusConstant.SuccessStatus;
                    response.Message = FaceRecognitionMessage.Success.DetectFaceSuccessfully;
                }
                else
                {
                    response.Errors.Add(FaceRecognitionMessage.Error.ThereAreNoFaceDetectedInTheImage);
                }
                return response;
            } catch (Exception ex)
            {
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage;
                response.Errors.Add(ex.Message);
                return response;
            }
        }

        private async Task<MemoryStream> CloneToMemoryStreamAsync(Stream originalStream)
        {
            originalStream.Position = 0;
            var memStream = new MemoryStream();
            await originalStream.CopyToAsync(memStream);
            memStream.Position = 0;
            return memStream;
        }

        private async Task<MemoryStream> ToMemoryStreamAsync(IFormFile file)
        {
            var memoryStream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(memoryStream);
            return memoryStream;
        }

        private async Task<DetectFacesResponse> DetectFacesFromAWSAsync(MemoryStream imageStream)
        {
            return await _rekognition.DetectFacesAsync(new Amazon.Rekognition.Model.DetectFacesRequest
            {
                Image = new Amazon.Rekognition.Model.Image { Bytes = imageStream },
                Attributes = new List<string> { "ALL" }
            });
        }

        private Font LoadFont()
        {
            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "fonts", "Arial.ttf");
            var fontCollection = new FontCollection();
            var fontFamily = fontCollection.Add(fontPath);
            return fontFamily.CreateFont(20, FontStyle.Bold);
        }

        private DrawingOptions GetDrawingOptions()
        {
            return new DrawingOptions
            {
                GraphicsOptions = new GraphicsOptions { Antialias = false }
            };
        }


        private async Task<DetectedFaceInformation?> ProcessFaceDetectionAsync(Image<Rgba32> image, FaceDetail face, Font font, DrawingOptions drawOptions)
        {
            int imgWidth = image.Width;
            int imgHeight = image.Height;

            var box = face.BoundingBox;
            int left = (int)(box.Left * imgWidth);
            int top = (int)(box.Top * imgHeight);
            int width = (int)(box.Width * imgWidth);
            int height = (int)(box.Height * imgHeight);

            var rect = new RectangularPolygon(left, top, width, height);
            var textPosition = new SixLabors.ImageSharp.PointF(left, top - 20);

            var cropped = image.Clone(x => x.Crop(new Rectangle(left, top, width, height)));

            using var faceStream = new MemoryStream();
            await cropped.SaveAsJpegAsync(faceStream);
            faceStream.Position = 0;

            try
            {
                var match = await SearchFaceAsync(faceStream);
                string username = match?.Username ?? FaceRecognitionConstant.UnKnownFace;
                string faceId = match?.FaceId ?? FaceRecognitionConstant.UnKnownFace;
                float confidence = (float)(match?.Confidence ?? 0);
                var color = match != null ? Color.Green : Color.Red;

                image.Mutate(ctx =>
                {
                    ctx.Draw(drawOptions, color, 3, rect);

                    var textOptions = new TextOptions(font)
                    {
                        WrappingLength = 200,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    // Tính kích thước dòng chữ
                    var textSize = TextMeasurer.MeasureSize(username, textOptions);

                    // Tạo background rectangle (màu nền nhạt hơn text)
                    var backgroundRect = new RectangularPolygon(left, top - textSize.Height - 5, textSize.Width + 10, textSize.Height + 5);

                    // Vẽ background (hơi bo padding 5px)
                    ctx.Fill(SixLabors.ImageSharp.Color.Black.WithAlpha(0.6f), backgroundRect);

                    // Vẽ text đè lên
                    ctx.DrawText(username, font, SixLabors.ImageSharp.Color.White, new PointF(left + 5, top - textSize.Height - 2));
                });

                return new DetectedFaceInformation
                {
                    UserId = match?.UserId ?? FaceRecognitionConstant.UnKnownFace,
                    Username = username,
                    FaceId = faceId,
                    Confidence = confidence,
                    BoundingBox = new FaceBoundingBox
                    {
                        Left = box.Left,
                        Top = box.Top,
                        Width = box.Width,
                        Height = box.Height
                    }
                };
            }
            catch 
            {
                return null;
            }
        }

        private async Task<DetectedFaceInformation?> SearchFaceAsync(Stream faceStream)
        {
            var searchResponse = await _rekognition.SearchFacesByImageAsync(new SearchFacesByImageRequest
            {
                CollectionId = CollectionId,
                Image = new Amazon.Rekognition.Model.Image { Bytes = new MemoryStream(((MemoryStream)faceStream).ToArray()) },
                FaceMatchThreshold = 90F,
                MaxFaces = 1
            });

            if (searchResponse.FaceMatches.Count == 0) return null;

            var match = searchResponse.FaceMatches[0];
            var user = await _userFaceRepository.GetUserByFaceIdAsync(match.Face.FaceId);
            if (user == null) return null;

            return new DetectedFaceInformation
            {
                UserId = user.UserId,
                Username = user.Username,
                FaceId = match.Face.FaceId,
                Confidence = match.Similarity
            };
        }


        private async Task<string> ConvertImageToBase64(Image<Rgba32> image)
        {
            using var outputStream = new MemoryStream();
            await image.SaveAsJpegAsync(outputStream);
            return $"data:image/jpeg;base64,{Convert.ToBase64String(outputStream.ToArray())}";
        }
        #endregion

        #region delete registerd face 
        public async Task<ApiResponseModel<object>> DeleteRegisteredFaceAsync(int userFaceId)
        {
            var response = new ApiResponseModel<object>
            {
                Data = null,
                Message = FaceRecognitionMessage.Error.RegisterUserFaceFailed,
                Errors = new List<string>()
            };

            // Kiểm tra quyền manager
            var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
            var manager = await _managerRepository.GetManagerByUserIdAsync(currentUserId);
            if (manager == null)
            {
                response.Errors.Add(FaceRecognitionMessage.Error.ManagerNotFound);
                return response;
            }

            // Kiểm tra user và manager cùng team
            var oldFace = await _userFaceRepository.GetUserFaceByIdAsync(userFaceId);
            if (oldFace == null)
            {
                response.Errors.Add(FaceRecognitionMessage.Error.RegisteredFaceNotFound);
                return response;
            }

            var user = await _userRepository.GetUserWithRoleByIdAsync(oldFace.UserId);
            if (user == null)
            {
                response.Errors.Add(FaceRecognitionMessage.Error.UserNotFound);
                return response;
            }

            bool isSameTeam = user.RoleCode switch
            {
                RoleCodeConstant.PlayerCode => user.Player?.TeamId == manager.TeamId,
                RoleCodeConstant.CoachCode => user.Coach?.TeamId == manager.TeamId,
                _ => false
            };

            if (!isSameTeam || !user.IsEnable)
            {
                response.Errors.Add(FaceRecognitionMessage.Error.YouCanOnlyDeleteFaceForYourTeamMember);
                return response;
            }

            // Remove registered face on aws and image file in cloud
            await RemoveRegisteredFace(oldFace.ImageUrl, new List<string> { oldFace.RegisteredFaceId });

            // Remove registered face in db
            await _userFaceRepository.DeleteRegisteredFaceAsync(oldFace);

            response.Status = ApiResponseStatusConstant.SuccessStatus;
            response.Message = FaceRecognitionMessage.Success.DeleteRegisteredFaceSuccessfully;

            return response;
        }

        public async Task<ApiResponseModel<UserRegisteredFacesDto>> GetRegisteredFacesForPlayerAsync(string userId)
        {
            var response = new ApiResponseModel<UserRegisteredFacesDto>();
            try
            {
                var user = await _userRepository.GetUserWithRoleByIdAsync(userId);
                if (user == null)
                {
                    response.Errors = new List<string> { FaceRecognitionMessage.Error.UserNotFound };
                    return response;
                }

                var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
                var manager = await _managerRepository.GetManagerByUserIdAsync(currentUserId);
                if (manager == null)
                {
                    response.Errors = new List<string> { FaceRecognitionMessage.Error.ManagerNotFound };
                    return response;
                }

                var userTeamId = string.Empty;
                if (user.RoleCode == RoleCodeConstant.CoachCode)
                {
                    userTeamId = user.Coach?.TeamId ?? string.Empty;
                }

                if (user.RoleCode == RoleCodeConstant.PlayerCode)
                {
                    userTeamId = user.Player?.TeamId ?? string.Empty;
                }

                if (userTeamId != manager.TeamId)
                {
                    response.Errors = new List<string> { FaceRecognitionMessage.Error.YouCanOnlyViewRegisteredFaceOfYourTeam };
                    return response;
                }

                var registeredFaces = await _userFaceRepository.GetRegisteredFacesByUserIdAsync(userId);
                var userRegisteredFaces = new UserRegisteredFacesDto
                {
                    UserId = userId,
                    Username = user.Username ?? string.Empty,
                    Fullname = user.Fullname ?? string.Empty,
                    UserFaces = registeredFaces?.Select(face => new UserFaceDto
                    {
                        UserId = face.UserId,
                        UserFaceId = face.UserFaceId,
                        RegisteredFaceId = face.RegisteredFaceId,
                        ImageUrl = face.ImageUrl,
                        RegisteredAt = face.RegisteredAt
                    }).ToList() ?? new List<UserFaceDto>()
                };

                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage;
                response.Data = userRegisteredFaces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage;
            }
            return response;
        }
        #endregion

        #region registered faces of a team
        public async Task<ApiResponseModel<List<UserRegisteredFacesDto>>> GetRegisteredFacesForTeamAsync(string teamId)
        {
            var response = new ApiResponseModel<List<UserRegisteredFacesDto>> ();
            try
            {
                var currentUserId = TokenHelper.GetCurrentUserId(_httpContextAccessor);
                var manager = await _managerRepository.GetManagerByUserIdAsync(currentUserId);
                if (manager == null)
                {
                    response.Errors = [FaceRecognitionMessage.Error.ManagerNotFound];
                    return response;
                }

                if (teamId != manager.TeamId)
                {
                    response.Errors = [FaceRecognitionMessage.Error.YouCanOnlyViewRegisteredFaceOfYourTeam];
                    return response;
                }

                var registeredFaces = await _userFaceRepository.GetRegisteredFacesByTeamIdIdAsync(teamId);
                var teamRegisteredFaces = new List<UserRegisteredFacesDto>();
                if (registeredFaces != null && registeredFaces.Count > 0)
                {
                    teamRegisteredFaces = registeredFaces
                    .GroupBy(face => face.UserId)
                    .Select(group =>
                    {
                        var firstFace = group.First();
                        return new UserRegisteredFacesDto
                        {
                            UserId = firstFace.UserId,
                            Username = firstFace.User.Username,
                            Fullname = firstFace.User.Fullname,
                            UserFaces = group.Select(face => new UserFaceDto
                            {
                                UserId = face.UserId,
                                UserFaceId = face.UserFaceId,
                                RegisteredFaceId = face.RegisteredFaceId,
                                ImageUrl = face.ImageUrl,
                                RegisteredAt = face.RegisteredAt
                            }).ToList()
                        };
                    }).ToList();
                }
                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = ApiResponseMessage.ApiResponseSuccessMessage.ApiSuccessMessage;
                response.Data = teamRegisteredFaces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                response.Status = ApiResponseStatusConstant.FailedStatus;
                response.Message = ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage;
            }
            return response;
        }
        #endregion
    }
}

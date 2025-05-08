using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.Chatbot;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Infrastructure.Repositories;
using BasketballAcademyManagementSystemAPI.Models;

namespace BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl
{
    public class ChatbotService : IChatbotService
    {
        private readonly IClaudeService _claudeService;
        private readonly IEmbeddingService _embeddingService;
        private readonly IQdrantService _qdrantService;
        private readonly ICachedQuestionRepository _cachedQuestionRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ChatbotService(IClaudeService claudeService
            , IEmbeddingService embeddingService
            , IQdrantService qdrantService
            , ICachedQuestionRepository cachedQuestionRepository
            , IWebHostEnvironment webHostEnvironment)
        {
            _claudeService = claudeService;
            _embeddingService = embeddingService;
            _qdrantService = qdrantService;
            _cachedQuestionRepository = cachedQuestionRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ApiResponseModel<ChatbotResponse>> AskAsync(ChatbotRequest request, string useFor)
        {
            var response = new ApiResponseModel<ChatbotResponse>()
            {
                Status = ApiResponseStatusConstant.SuccessStatus
            };

            // Lấy ra câu hỏi tương đồng đã được hỏi trong cache
            var relevantQuestion = await _cachedQuestionRepository.GetRelevantQuestionAsync(request.Question, useFor);

            if (relevantQuestion != null)
            {
                response.Data = new ChatbotResponse
                {
                    Question = request.Question,
                    Answer = relevantQuestion.Answer
                };
                response.Message = ChatbotMessage.Success.FoundRevelentQuestionInCache;
                return response;
            }

            // Nếu không tìm thấy câu hỏi tương đồng trong cache, thực hiện tìm kiếm trên Qdrant
            var questionVector = await _embeddingService.GetEmbeddingAsync(request.Question);
            var context = await _qdrantService.SearchRelevantContextAsync(questionVector, useFor);

            // Nếu không tìm thấy thông tin trong Qdrant, trả về thông báo
            if (context == null)
            {
                response.Data = new ChatbotResponse
                {
                    Question = request.Question,
                    Answer = ChatbotMessage.Error.NotFoundAnswerForThisQuestion
                };
                response.Message = ChatbotMessage.Error.NotFoundRelevantInformationInHandbook;
                return response;
            }

            // Thực hiện gọi đến Claude để lấy câu trả lời
            string prompt = ChatbotConstant.GetFullPrompt(context, request.Question);
            var answer = await _claudeService.AskAsync(prompt);

            // Lưu câu hỏi và câu trả lời vào cache
            await _cachedQuestionRepository.AddQuestionAndAnswerToCacheAsync(new CachedQuestion
            {
                Question = request.Question,
                Answer = answer,
                IsForGuest = useFor == ChatbotConstant.UseForGuest
            });

            response.Data = new ChatbotResponse
            {
                Question = request.Question,
                Answer = answer
            };
            response.Message = ChatbotMessage.Success.AskByGuestSuccess;
            return response;
        }

        public async Task<ApiResponseModel<UpdateChatbotDocumentResponse>> UpdateChatBotDocument(UpdateChatbotDocumentRequest request)
        {
            var response = new ApiResponseModel<UpdateChatbotDocumentResponse>()
            {
                Message = ChatbotMessage.Error.UpdateChatbotDocumentFailed,
                Status = ApiResponseStatusConstant.FailedStatus,
            };

            // 1. Kiểm tra file có tồn tại không và có phải là docx không
            if (request.DocumentFile == null || request.DocumentFile.ContentType != "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                response.Errors = [ChatbotMessage.Error.DocumentFileTypeNotSupported];
                return response;
            }

            // 2.Đọc nội dung file và chunk
            using var stream = request.DocumentFile.OpenReadStream();
            var paragraphs = WordHelper.ExtractChunksByMarker(stream);
            if (paragraphs.Count == 0)
            {
                response.Errors = [ChatbotMessage.Error.DocumentEmpty];
                return response;
            }

            // 3. Embedding cho từng đoạn văn
            var vectors = new List<(string, float[])>();
            var semaphore = new SemaphoreSlim(1); // giới hạn 1 request đồng thời tránh nghẽn mạng
            var tasks = new List<Task>();
            foreach (var p in paragraphs)
            {
                await semaphore.WaitAsync();
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        Console.WriteLine("Embedding paragraph: " + p);
                        var vector = await _embeddingService.GetEmbeddingAsync(p);
                        lock (vectors)
                        {
                            vectors.Add((p, vector));
                        }
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }
            await Task.WhenAll(tasks);

            if (vectors.Count == 0)
            {
                response.Errors = [ChatbotMessage.Error.DocumentEmpty];
                return response;
            }

            // 4. Xoá toàn bộ vector cũ và cache SQL
            await _qdrantService.DeleteAllAsync(request.UseFor);
            await _cachedQuestionRepository.DeleteAllAsync(request.UseFor);

            // 5. Insert vào Qdrant
            var isInsertSuccess = await _qdrantService.InsertChunksAsync(vectors, request.UseFor);
            if (!isInsertSuccess)
            {
                response.Errors = [ChatbotMessage.Error.ErrorWhenSavingDocument];
            }
            else
            {
                // Lưu tài liệu vào /uploads/documents/chatbots
                await SaveChatbotDocumentAsync(request.DocumentFile, request.UseFor);

                response.Status = ApiResponseStatusConstant.SuccessStatus;
                response.Message = ChatbotMessage.Success.UpdateChatbotDocumentSuccess;

                // Lưu thông tin tài liệu vào response
                var savedFileName = request.UseFor == ChatbotConstant.UseForGuest
                    ? ChatbotConstant.ChatbotDocumentForGuestFileName
                    : "Unknown";
                response.Data = new UpdateChatbotDocumentResponse()
                {
                    DocumentName = request.DocumentFile.FileName,
                    UseFor = request.UseFor,
                    DocumentUrl = Path.Combine(ChatbotConstant.ChatbotDocumentFolderPath, savedFileName).TrimStart('/'),
                };
            }
            return response;
        }

        public async Task SaveChatbotDocumentAsync(IFormFile file, string useFor)
        {
            string fileName = null;
            if (useFor == ChatbotConstant.UseForGuest)
            {
                fileName = ChatbotConstant.ChatbotDocumentForGuestFileName;
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }

            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, ChatbotConstant.ChatbotDocumentFolderPath.TrimStart('/'), fileName);

            var directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task<ApiResponseModel<FileResponse>> DownloadChatbotDocumentAsync()
        {
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            var mainFilePath = Path.Combine(wwwRootPath, "uploads", "documents", "chatbot", "YHBT_So_Tay.docx");
            var defaultFilePath = Path.Combine(wwwRootPath, "uploads", "documents", "chatbot", "YHBT_So_Tay_Mau.docx");

            string filePath;
            if (System.IO.File.Exists(mainFilePath))
            {
                filePath = mainFilePath;
            }
            else if (System.IO.File.Exists(defaultFilePath))
            {
                filePath = defaultFilePath;
            }
            else
            {
                return new ApiResponseModel<FileResponse>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = "Không tìm thấy file nào."
                };
            }

            var fileContent = await System.IO.File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);
            var contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

            return new ApiResponseModel<FileResponse>
            {
                Status = ApiResponseStatusConstant.SuccessStatus,
                Data = new FileResponse
                {
                    FileContent = fileContent,
                    FileName = fileName,
                    ContentType = contentType
                }
            };
        }
    }
}

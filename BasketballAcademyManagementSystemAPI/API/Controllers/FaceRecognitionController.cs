using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.FaceRecognition;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Application.Services.ServiceImpl;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using BasketballAcademyManagementSystemAPI.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BasketballAcademyManagementSystemAPI.Common.Messages.GeneralServerMessage;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/face-recognition")]
    [ApiController]
    public class FaceRecognitionController : ControllerBase
    {
        private readonly IFaceRecognitionService _faceService;

        public FaceRecognitionController(IFaceRecognitionService faceService)
        {
            _faceService = faceService;
        }

        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterFacesRequest request)
        {
            try
            {
                var response = await _faceService.RegisterFacesForAPlayerAsync(request.UserId, request.Image);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        [HttpPost("detect")]
        public async Task<IActionResult> Detect([FromForm] DetectFacesRequest request)
        {
            try
            {
                var response = await _faceService.DetectFacesInGroupAsync(request.Image);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        [HttpDelete("delete/{userFaceId}")]
        public async Task<IActionResult> Delete(int userFaceId)
        {
            try
            {
                var response = await _faceService.DeleteRegisteredFaceAsync(userFaceId);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        [HttpGet("registered-faces")]
        public async Task<IActionResult> GetRegisteredFaces(string? userId, string? teamId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(teamId))
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage,
                        Errors = [FaceRecognitionMessage.Error.UserIdOrTeamIdRequired]
                    });
                }

                if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(teamId))
                {
                    return BadRequest(new ApiResponseModel<object>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage,
                        Errors = [FaceRecognitionMessage.Error.OnlyUserIdOrTeamIdAllowed]
                    });
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    var response = await _faceService.GetRegisteredFacesForPlayerAsync(userId);
                    if (response.Status == ApiResponseStatusConstant.FailedStatus)
                    {
                        return BadRequest(response);
                    }
                    return Ok(response);
                }

                if (!string.IsNullOrEmpty(teamId))
                {
                    var response = await _faceService.GetRegisteredFacesForTeamAsync(teamId);
                    if (response.Status == ApiResponseStatusConstant.FailedStatus)
                    {
                        return BadRequest(response);
                    }
                    return Ok(response);
                }

                return BadRequest(new ApiResponseModel<object>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ApiResponseMessage.ApiResponseErrorMessage.ApiFailedMessage,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

    }
}

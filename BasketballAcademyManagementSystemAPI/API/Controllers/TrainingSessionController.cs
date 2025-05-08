using BasketballAcademyManagementSystemAPI.Application.DTOs.CourtManagement;
using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TrainingSession;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BasketballAcademyManagementSystemAPI.Common.Messages.ApiResponseMessage;
using static BasketballAcademyManagementSystemAPI.Common.Messages.GeneralServerMessage;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/training-session")]
    [ApiController]
    public class TrainingSessionController : ControllerBase
    {
        private readonly ITrainingSessionService _trainingSessionService;
        private readonly IExerciseService _exerciseService;

        public TrainingSessionController(ITrainingSessionService trainingSessionService
            , IExerciseService exerciseService)
        {
            _trainingSessionService = trainingSessionService;
            _exerciseService = exerciseService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTrainingSessions(DateTime startDate, DateTime endDate, string? teamId, string? courtId)
        {
            try
            {
                var response = await _trainingSessionService.GetTrainingSessionsAsync(startDate, endDate, teamId, courtId);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        #region create training session

        [HttpPost("create-additional")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> CreateAdditionalTrainingSession([FromBody] CreateTrainingSessionRequest request)
        {
            try
            {
                var response = await _trainingSessionService.CreateAddtitionalTrainingSessionAsync(request);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpGet("pending")]
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetPendingTrainingSessions()
        {
            try
            {
                var response = await _trainingSessionService.GetPendingTrainingSession();
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

        [HttpPost("approve")]
        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        public async Task<IActionResult> ApproveTrainingSession(ApproveTrainingSessionRequest request)
        {
            try
            {
                var response = await _trainingSessionService.ApproveTrainingSessionAsync(request);
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

        [HttpPost("reject")]
        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        public async Task<IActionResult> RejectTrainingSession(CancelTrainingSessionRequest request)
        {
            try
            {
                var response = await _trainingSessionService.RejectTrainingSessionAsync(request);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpGet("check-conflict")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> CheckTrainingSessionConflict([FromQuery] CheckTrainingSessionConflictRequest request)
        {
            try
            {
                var response = await _trainingSessionService.CheckTrainingSessionConflictAsync(request);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }


        [HttpGet("{trainingSessionId}")]
        [Authorize]
        public async Task<IActionResult> GetTrainingSessionById(string trainingSessionId)
        {
            try
            {
                var response = await _trainingSessionService.GetTrainingSessionByIdAsync(trainingSessionId);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }
        #endregion


        #region cancel training session
        [HttpPost("cancel")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> RequestToCancelTrainingSession(CancelTrainingSessionRequest request)
        {
            try
            {
                var result = await _trainingSessionService.RequestToCancelTrainingSessionAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpGet("cancel-request")]
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetRequestCancelTrainingSession()
        {
            try
            {
                var result = await _trainingSessionService.GetRequestCancelTrainingSession();
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPost("{trainingSessionId}/cancel/approve")]
        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        public async Task<IActionResult> ApproveTrainingSessionCancelRequest(string trainingSessionId)
        {
            try
            {
                var result = await _trainingSessionService.CancelTrainingSessionAsync(trainingSessionId);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPost("cancel/reject")]
        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        public async Task<IActionResult> RejectCancelTrainingSessionRequest(CancelTrainingSessionChangeStatusRequest request)
        {
            try
            {
                var result = await _trainingSessionService.RejectCancelTrainingSessionRequestAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        #endregion

        #region update training session
        [HttpPut("update")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> RequestUpdateTrainingSession([FromBody] UpdateTrainingSessionRequest request)
        {
            try
            {
                var result = await _trainingSessionService.RequestToUpdateTrainingSessionAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi yêu cầu cập nhật buổi tập: " + ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpGet("update-request")]
        [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.CoachCode}")]
        public async Task<IActionResult> GetPendingRequestUpdateTrainingSession()
        {
            try
            {
                var result = await _trainingSessionService.GetPendingRequestUpdateTrainingSession();
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPost("update/approve")]
        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        public async Task<IActionResult> ApproveUpdateTrainingSession(ApproveTrainingSessionRequest request)
        {
            try
            {
                var result = await _trainingSessionService.UpdateTrainingSessionAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPost("update/reject")]
        [Authorize(Roles = RoleCodeConstant.ManagerCode)]
        public async Task<IActionResult> RejectUpdateTrainingSession([FromBody] CancelTrainingSessionChangeStatusRequest request)
        {
            try
            {
                var result = await _trainingSessionService.RejectUpdateTrainingSessionRequestAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }
        #endregion

        [HttpGet("available-courts")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> GetAvailableCourts([FromQuery] GetAvailableCourtsRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponseModel<List<CourtDto>>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = ApiResponseErrorMessage.ApiFailedMessage,
                        Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
                    });
                }

                var response = await _trainingSessionService.GetAvailableCourtsAsync(request);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPost("generate")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> GenerateTrainingSessions([FromBody] GenerateTrainingSessionsRequest request)
        {
            try
            {
                var response = await _trainingSessionService.GenerateTrainingSessionsAsync(request);
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

        [HttpPost("bulk-create")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> CreateTrainingSessions([FromBody] List<CreateTrainingSessionRequest> requests)
        {
            try
            {
                var response = await _trainingSessionService.BulkCreateTrainingSessionsAsync(requests);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPost("add-exercise")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> AddExercise([FromBody] CreateExerciseRequest request)
        {
            try
            {
                var result = await _exerciseService.AddExerciseForTrainingSessionAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpPut("edit-exercise")]
        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        public async Task<IActionResult> EditExercise([FromBody] UpdateExerciseRequest request)
        {
            try
            {
                var result = await _exerciseService.UpdateExerciseAsync(request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpDelete("{exerciseId}")]
        public async Task<IActionResult> RemoveExercise(string exerciseId)
        {
            try
            {
                var response = await _exerciseService.RemoveExerciseAsync(exerciseId);
                if (response.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

    }
}

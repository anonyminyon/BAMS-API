using BasketballAcademyManagementSystemAPI.Application.DTOs;
using BasketballAcademyManagementSystemAPI.Application.DTOs.TryOutScore;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/try-out-score")]
    [ApiController]
    [Authorize(Roles = $"{RoleCodeConstant.ManagerCode},{RoleCodeConstant.CoachCode},{RoleCodeConstant.PresidentCode}")]
    public class TryOutScoreController : ControllerBase
    {
        private readonly ITryOutMeasurementScaleService _tryOutMeasurementScaleService;
        private readonly ITryOutScorecardService _tryOutScorecardService;


        public TryOutScoreController(ITryOutMeasurementScaleService tryOutMeasurementScaleService
            , ITryOutScorecardService tryOutScorecardService)
        {
            _tryOutMeasurementScaleService = tryOutMeasurementScaleService;
            _tryOutScorecardService = tryOutScorecardService;
        }

        // Lấy toàn bộ danh sách kỹ năng theo dạng cây
        [HttpGet("measurement-scale")]
        public async Task<ActionResult<List<TryOutMeasurementScaleDto>>> GetSkillTree()
        {
            var skills = await _tryOutMeasurementScaleService.GetSkillTreeAsync();
            return Ok(skills);
        }

        // Lấy 1 kỹ năng theo MeasurementScaleCode
        [HttpGet("measurement-scale/{measurementScaleCode}")]
        public async Task<ActionResult<TryOutMeasurementScaleDto>> GetSkillByCode(string measurementScaleCode)
        {
            var skill = await _tryOutMeasurementScaleService.GetSkillByCodeAsync(measurementScaleCode);
            if (skill == null) return NotFound();
            return Ok(skill);
        }

        [HttpGet("measurement-scale/leaf")]
        public async Task<ActionResult<List<TryOutMeasurementScaleDto>>> GetLeafSkills()
        {
            var skills = await _tryOutMeasurementScaleService.GetLeafSkillsAsync();
            return Ok(skills);
        }

        [HttpPost("add-scores")]
        public async Task<ApiResponseModel<string>> AddScores([FromBody] BulkPlayerScoreInputDto input)
        {
            return await _tryOutScorecardService.AddOrUpdateScoresAsync(input);
        }

        [HttpPost("add-single-player-scores")]
        public async Task<ApiResponseModel<string>> AddOrUpdateSinglePlayerScores([FromBody] PlayerScoreInputDto input)
        {
            try
            {
                var apiResponse = await _tryOutScorecardService.AddOrUpdateScoresAsync(new BulkPlayerScoreInputDto { Players = new List<PlayerScoreInputDto> { input } });
                if (apiResponse.Status == ApiResponseStatusConstant.SuccessStatus)
                {
                    return apiResponse;
                }
                else
                {
                    return new ApiResponseModel<string>
                    {
                        Status = ApiResponseStatusConstant.FailedStatus,
                        Message = apiResponse.Message,
                        Errors = apiResponse.Errors
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponseModel<string>
                {
                    Status = ApiResponseStatusConstant.FailedStatus,
                    Message = ex.Message,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        [HttpPut("update-scores")]
        public async Task<ApiResponseModel<string>> UpdateScores([FromBody] BulkPlayerScoreInputDto input)
        {
            return await _tryOutScorecardService.UpdateScoresAsync(input);
        }

        [HttpPut("update-single-player-scores")]
        public async Task<IActionResult> UpdateSinglePlayerScores([FromBody] PlayerScoreInputDto input)
        {
            await _tryOutScorecardService.UpdateScoresAsync(new BulkPlayerScoreInputDto { Players = new List<PlayerScoreInputDto> { input } });
            return Ok();
        }

        [HttpGet("player/score/{playerRegistrationId}")]
        public async Task<IActionResult> GetScoresByPlayerRegistrationId(int playerRegistrationId)
        {
            try
            {
                var response = await _tryOutScorecardService.GetScoresByPlayerRegistrationIdAsync(playerRegistrationId);
                if (response == null)
                {
                    return NotFound(response);
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerMessage.GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [HttpGet("player/report/{playerRegistrationId}")]
        public async Task<IActionResult> GetReportByPlayerRegistrationId(int playerRegistrationId)
        {
            var scores = await _tryOutScorecardService.GetReportByPlayerRegistrationIdAsync(playerRegistrationId);
            return Ok(scores);
        }

        [HttpGet("session/scores/{sessionId}")]
        public async Task<IActionResult> GetScoresBySessionId(int sessionId, [FromQuery] PlayerRegistrationScoresFilterDto filter)
        {
            var scores = await _tryOutScorecardService.GetScoresByRegistrationSessionIdAsync(sessionId, filter);
            return Ok(scores);
        }

        [HttpGet("session/report/{sessionId}")]
        public async Task<IActionResult> GetReportBySessionId(int sessionId, [FromQuery] PlayerRegistrationScoresFilterDto filter)
        {
            var response = await _tryOutScorecardService.GetReportByRegistrationSessionIdAsync(sessionId, filter);

            if (response.Status == ApiResponseStatusConstant.SuccessStatus)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("session/report/{sessionId}/export")]
        public async Task<IActionResult> ExportReportBySessionId(int sessionId, [FromQuery] bool? filter)
        {
            var response = await _tryOutScorecardService.GetPlayerRegistrationSessionScoreReportAsync(sessionId, filter);

            if (response.Data != null)
            {
                return File(response.Data.Data, response.Data.ContentType, response.Data.FileName);
            }

            return BadRequest(response);
        }
    }
}

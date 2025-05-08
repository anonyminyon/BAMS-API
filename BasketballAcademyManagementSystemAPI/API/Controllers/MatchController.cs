using BasketballAcademyManagementSystemAPI.Application.DTOs.Match;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BasketballAcademyManagementSystemAPI.Common.Messages.GeneralServerMessage;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/match")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly IMatchLineupService _matchLineupService;
        private readonly IMatchArticleService _matchArticleService;

        public MatchController(IMatchService matchService
            , IMatchLineupService matchLineupService
            , IMatchArticleService matchArticleService)
        {
            _matchService = matchService;
            _matchLineupService = matchLineupService;
            _matchArticleService = matchArticleService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMatchesInAWeek(DateTime startDate, DateTime endDate, string? teamId, string? courtId)
        {
            try
            {
                var response = await _matchService.GetMatchesInAWeekAsync(startDate, endDate, teamId, courtId);
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


        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] CreateMatchRequest matchCreateDto)
        {
            try
            {
                var apiResponse = await _matchService.CreateMatchAsync(matchCreateDto);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpPut("{matchId}")]
        public async Task<IActionResult> UpdateMatch(int matchId, [FromBody] UpdateMatchRequest request)
        {
            try
            {
                var result = await _matchService.UpdateMatchAsync(matchId, request);
                if (result.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }


        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpGet("available-courts")]
        public async Task<IActionResult> GetAvailableCourts([FromQuery] DateTime matchDate)
        {
            try
            {
                var apiResponse = await _matchService.GetAvailableCourtsAsync(matchDate);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize]
        [HttpGet("{matchId}")]
        public async Task<IActionResult> GetMatchDetail(int matchId)
        {
            try
            {
                var apiResponse = await _matchService.GetMatchDetailAsync(matchId);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return NotFound(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpPost("cancel/{matchId}")]
        public async Task<IActionResult> CancelMatch(int matchId)
        {
            try
            {
                var apiResponse = await _matchService.CancelMatchAsync(matchId);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpPost("call-player")]
        public async Task<IActionResult> CallPlayerForMatch([FromBody] CallPlayerForMatchRequest request)
        {
            try
            {
                var apiResponse = await _matchLineupService.CallPlayerForMatchAsync(request);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpGet("{matchId}/available-players")]
        public async Task<IActionResult> GetAvailablePlayers(int matchId)
        {
            try
            {
                var apiResponse = await _matchLineupService.GetAvailablePlayersAsync(matchId);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }


        [Authorize(Roles = RoleCodeConstant.CoachCode)]
        [HttpDelete("{matchId}/player/{playerId}")]
        public async Task<IActionResult> RemovePlayerFromMatchLineup(int matchId, string playerId)
        {
            try
            {
                var apiResponse = await _matchLineupService.RemovePlayerFromMatchLineupAsync(matchId, playerId);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        [HttpDelete("{matchId}/article/{articleId}")]
        public async Task<IActionResult> RemoveMatchArticle(int matchId, int articleId)
        {
            try
            {
                var apiResponse = await _matchArticleService.RemoveMatchArticleAsync(matchId, articleId);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        [HttpPost("{matchId}/articles")]
        public async Task<IActionResult> AddMatchArticles(int matchId, [FromBody] List<AddMatchArticleRequest> requests)
        {
            try
            {
                var apiResponse = await _matchArticleService.AddMatchArticlesAsync(matchId, requests);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        [HttpPost("{matchId}/upload-article-file")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadMatchArticleFile(int matchId, [FromForm] UploadMatchArticleFileRequest request)
        {
            try
            {
                var apiResponse = await _matchArticleService.UploadMatchArticleFileAsync(matchId, request);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles =
            $"{RoleCodeConstant.ManagerCode}," +
            $"{RoleCodeConstant.CoachCode}")]
        [HttpDelete("delete-article-file")]
        public IActionResult DeleteMatchArticleFile([FromQuery] string filePath)
        {
            try
            {
                var apiResponse = _matchArticleService.DeleteMatchArticleFile(filePath);
                if (apiResponse.Status == ApiResponseStatusConstant.FailedStatus)
                {
                    return BadRequest(apiResponse);
                }
                return Ok(apiResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

    }
}

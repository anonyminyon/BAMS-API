using BasketballAcademyManagementSystemAPI.Application.DTOs.ClubContact;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/club-contact")]
    [ApiController]
    public class ClubContactController : ControllerBase
    {
        private readonly IClubContactService _clubContactService;

        public ClubContactController(IClubContactService clubContactService)
        {
            _clubContactService = clubContactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClubContact()
        {
            try
            {
                var clubContactMethods = await _clubContactService.GetClubContactMethodsAsync();
                return Ok(clubContactMethods);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] UpdateClubContactDto updateClubContactDto)
        {
            try
            {
                var result = await _clubContactService.EditClubContactAsync(updateClubContactDto);
                return ApiResponseHelper.HandleApiResponse(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message });
            }
        }
    }
}

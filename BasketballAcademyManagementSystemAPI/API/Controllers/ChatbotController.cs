using BasketballAcademyManagementSystemAPI.Application.DTOs.Chatbot;
using BasketballAcademyManagementSystemAPI.Application.Services;
using BasketballAcademyManagementSystemAPI.Common.Constants;
using BasketballAcademyManagementSystemAPI.Common.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasketballAcademyManagementSystemAPI.API.Controllers
{
    [Route("api/chatbot")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotService _chatbotService;
        private readonly IQdrantService _qdrantService;

        public ChatbotController(IChatbotService chatbotService, IQdrantService qdrantService)
        {
            _chatbotService = chatbotService;
            _qdrantService = qdrantService;
        }


        [HttpPost("guest")]
        public async Task<IActionResult> AskByGuest([FromBody] ChatbotRequest request)
        {
            try
            {
                var response = await _chatbotService.AskAsync(request, ChatbotConstant.UseForGuest);
                if (response.Status == ApiResponseStatusConstant.SuccessStatus)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerMessage.GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        [HttpPost("update-chatbot-document")]
        public async Task<IActionResult> UpdateChatBotDocument(UpdateChatbotDocumentRequest request)
        {
            try
            {
                var response = await _chatbotService.UpdateChatBotDocument(request);
                if (response.Status == ApiResponseStatusConstant.SuccessStatus)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerMessage.GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        [HttpGet("download-chatbot-document")]
        public async Task<IActionResult> DownloadOldChatbotDocument()
        {
            try
            {
                var response = await _chatbotService.DownloadChatbotDocumentAsync();
                if (response.Status == ApiResponseStatusConstant.SuccessStatus && response.Data != null)
                {
                    return File(response.Data.FileContent, response.Data.ContentType, response.Data.FileName);
                }
                else
                {
                    return NotFound(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerMessage.GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }

        [Authorize(Roles = RoleCodeConstant.PresidentCode)]
        [HttpGet("chatbot-document-contents")]
        public async Task<IActionResult> ChatbotDocumentContent([FromQuery] string useFor)
        {
            try
            {
                var response = await _qdrantService.ListChatbotVectorsAsync(useFor);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, GeneralServerMessage.GeneralServerErrorMessage.SomeThingWentWrong);
            }
        }
    }
}

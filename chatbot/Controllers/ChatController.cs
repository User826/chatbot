using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenAiBackend.Models;
using OpenAiBackend.Services;

namespace OpenAiBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly OpenAiService _openAIService;
        private readonly ILogger<ChatController> _logger;
        private readonly string _info ;
        private readonly string _infoSeedFilePath = "Resources/infoSeedFile.txt";
        public ChatController(OpenAiService openAIService, ILogger<ChatController> logger)
        {
            _openAIService = openAIService;
            _logger = logger;
            _info = System.IO.File.ReadAllText(_infoSeedFilePath);
        }

        [HttpPost]
        public async Task<IActionResult> ChatPotOtherQuestionPost([FromBody] ChatRequest request)
        {
            try
            {
                _logger.LogInformation("Received request with prompt: {Prompt}", request.Prompt);
                string modifiedPrompt = _info + "\n" + request.Prompt;
                var response = await _openAIService.SendRequestToChatGpt(modifiedPrompt);
                _logger.LogInformation("Received response from OpenAI: {Response}", response);
                return Ok(new { response });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Request to OpenAI API failed.");
                return StatusCode(500, "Internal server error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
    
}
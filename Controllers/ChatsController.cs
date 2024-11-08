using GladLogsApi.Configuration.AuthConfigurations;
using GladLogsApi.Data.Services.ChatService;
using GladLogsApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GladLogsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatsController : ControllerBase
    {

        private readonly IChatService _chatService;


        
        private readonly IOptions<AuthConfig> _authConfig;

        public ChatsController(IChatService chatService, IOptions<AuthConfig> authConfig )
        {
            _chatService = chatService;
            _authConfig = authConfig;

        }


        [HttpGet]
        public async Task<IActionResult> GetAllChatsAsync()
        {
            var chats = await _chatService.GetAllChatsAsync();
            if (chats is null)
            {
                return NotFound();
            }
            return Ok(chats);
        }


        //I belive this doesn't need Try/Catch because the service and repo already has it
        // POST: Chats/
        [HttpPost]
        public async Task<IActionResult> CreateChatAsync( string Chatname, string AuthKey)
        {
            //Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Check if the AuthKey is valid
            if (AuthKey != _authConfig.Value.AuthKey)
            {
                return Unauthorized();
            }

            var chat = await _chatService.CreateChatAsync(Chatname);
            if (chat is null)
            {
                return StatusCode(500);
            }
            return Ok(chat);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteChatAsync(Guid Id, string AuthKey)
        {
            //Check if the AuthKey is valid
            if (AuthKey != _authConfig.Value.AuthKey)
            {
                return Unauthorized();
            }

            var chat = await _chatService.DeleteChatAsync(Id);
            if (chat is null)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}

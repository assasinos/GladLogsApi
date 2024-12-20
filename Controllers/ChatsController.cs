﻿using GladLogsApi.Attributes;
using GladLogsApi.Configuration.ConfigTypes;
using GladLogsApi.Data.Services.ChatService;
using GladLogsApi.Data.Services.TwitchConnectionService;
using GladLogsApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;



namespace GladLogsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly ITwitchConnectionService _twitchConnectionService;

        public ChatsController(IChatService chatService, ITwitchConnectionService twitchConnectionService)
        {
            _twitchConnectionService = twitchConnectionService;
            _chatService = chatService;
        }

        /// <summary>
        /// Retrieves all chats.
        /// </summary>
        /// <returns>A collection of chat DTOs.</returns>
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

        /// <summary>
        /// Creates a new chat.
        /// </summary>
        /// <param name="Chatname">The name of the chat to create.</param>
        /// <returns>The created chat DTO.</returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidateAuthKeyAttribute))]
        public async Task<IActionResult> CreateChatAsync(string Chatname)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var chat = await _chatService.CreateChatAsync(Chatname);
            if (chat is null)
            {
                return StatusCode(500);
            }
            //restart the service
            if(!_twitchConnectionService.Restart())
            {
                return BadRequest("Twitch Connection Service couldn't be restarted, please try to do it mannualy");
            }
            return Ok(chat);
        }

        /// <summary>
        /// Deletes a chat.
        /// </summary>
        /// <param name="Id">The unique identifier of the chat to delete.</param>
        /// <returns>A status indicating the result of the operation.</returns>
        [HttpDelete]
        [ServiceFilter(typeof(ValidateAuthKeyAttribute))]
        public async Task<IActionResult> DeleteChatAsync(string Chatname)
        {
            var chat = await _chatService.DeleteChatAsync(Chatname);
            if (chat is null)
            {
                return BadRequest();
            }
            //restart the service
            if (!_twitchConnectionService.Restart())
            {
                return BadRequest("Twitch Connection Service couldn't be restarted, please try to do it mannualy");
            }
            return Ok();
        }
    }
}

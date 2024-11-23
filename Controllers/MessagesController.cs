using GladLogsApi.Data.Services.MessageService;
using GladLogsApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GladLogsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagesController : ControllerBase
    {

        private readonly IMessageService _messageService;
        private readonly ILogger<MessagesController> _logger;

        public MessagesController(IMessageService messageService, ILogger<MessagesController> logger)
        {
            _messageService = messageService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the count of messages for a specific user in a specific chat.
        /// </summary>
        /// <param name="UserId">The ID of the user.</param>
        /// <param name="ChatId">The ID of the chat.</param>
        /// <returns>The count of messages for the user in the chat.</returns>
        // GET /messages/count?UserId=username&ChatId=chatname
        [HttpGet("count")]
        public IActionResult GetUserMessageCountByChat(string UserId, string ChatId)
        {
            var count = _messageService.GetUserMessageCountByChat(UserId, ChatId);

            if (count is null)
            {
                _logger.LogError("Failed to get message count for user {UserId} in chat {ChatId}", UserId, ChatId);
                return BadRequest();
            }

            return Ok(count);
        }





        /// <summary>
        /// Gets the messages for a specific user in a specific chat for a specific week.
        /// </summary>
        /// <param name="UserId">The ID of the user.</param>
        /// <param name="WeekId">The ID of the week.</param>
        /// <param name="ChatId">The ID of the chat.</param>
        /// <returns>The messages for the user in the chat for the specified week.</returns>
        // GET /messages?UserId=username&WeekId=00000000-0000-0000-0000-000000000000&ChatId=chatname
        [HttpGet]
        public IActionResult GetUserMessagesByChatAndWeek(string UserId, Guid WeekId, string ChatId)
        {
            var messages = _messageService.GetUserShortMessagesByChatAndWeek(UserId, WeekId, ChatId);

            if (messages is null)
            {
                _logger.LogError("Failed to get messages for user {UserId} in chat {ChatId} for week {WeekId}", UserId, ChatId, WeekId);
                return BadRequest();
            }

            return Ok(messages);
        }


    }
}

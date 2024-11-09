using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Entities;

namespace GladLogsApi.Data.Services.MessageService
{
    /// <summary>
    /// Interface for message service operations.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Asynchronously creates a new message.
        /// </summary>
        /// <param name="message">The message to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created message DTO.</returns>
        Task<MessageDto?> CreateMessageAsync(CreateMessageDto message);

        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="message">The message to create.</param>
        /// <returns>The created message DTO.</returns>
        MessageDto? CreateMessage(CreateMessageDto message);

        /// <summary>
        /// Gets the messages of a user by chat and week.
        /// </summary>
        /// <param name="UserId">The user ID.</param>
        /// <param name="WeekId">The week ID.</param>
        /// <param name="ChatId">The chat ID.</param>
        /// <returns>A collection of message DTOs.</returns>
        ICollection<MessageDto>? GetUserMessagesByChatAndWeek(Guid UserId, Guid WeekId, Guid ChatId);

        /// <summary>
        /// Asynchronously gets the messages of a user by chat and week.
        /// </summary>
        /// <param name="UserId">The user ID.</param>
        /// <param name="WeekId">The week ID.</param>
        /// <param name="ChatId">The chat ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of message DTOs.</returns>
        Task<ICollection<MessageDto>?> GetUserMessagesByChatAndWeekAsync(Guid UserId, Guid WeekId, Guid ChatId);

        /// <summary>
        /// Asynchronously gets the count of messages of a user by chat.
        /// </summary>
        /// <param name="UserId">The user ID.</param>
        /// <param name="ChatId">The chat ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of messages.</returns>
        Task<int?> GetUserMessageCountByChatAsync(Guid UserId, Guid ChatId);

        /// <summary>
        /// Gets the count of messages of a user by chat.
        /// </summary>
        /// <param name="UserId">The user ID.</param>
        /// <param name="ChatId">The chat ID.</param>
        /// <returns>The count of messages.</returns>
        int? GetUserMessageCountByChat(Guid UserId, Guid ChatId);
    }
}

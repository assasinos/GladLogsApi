using GladLogsApi.Models.Dtos;

namespace GladLogsApi.Data.Services.ChatService
{
    public interface IChatService
    {

        /// <summary>
        /// Creates a new chat asynchronously.
        /// </summary>
        /// <param name="createChatDto">The data transfer object containing the details of the chat to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created chat DTO, or null if the creation failed.</returns>
        Task<ChatDto?> CreateChatAsync(string ChatName);
        /// <summary>
        /// Deletes a chat asynchronously.
        /// </summary>
        /// <param name="Id">The unique identifier of the chat to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains successful task, or null if the deletion failed.</returns>
        Task<Task?> DeleteChatAsync(string Id);
        /// <summary>
        /// Retrieves all chats asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of chat DTOs, or null if no chats were found.</returns>
        Task<ICollection<ChatDto>?> GetAllChatsAsync();
    }
}

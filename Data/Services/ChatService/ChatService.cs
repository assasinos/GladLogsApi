using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Entities;

namespace GladLogsApi.Data.Services.ChatService
{
    public class ChatService : IChatService
    {

        private readonly ILogger<ChatService> _logger;
        private readonly ICrudRepository<string,Chat,ChatDto,CreateChatDto> _chatRepository;

        public ChatService(ILogger<ChatService> logger, ICrudRepository<string, Chat, ChatDto, CreateChatDto> chatRepository)
        {
            _logger = logger;
            _chatRepository = chatRepository;
        }


        public async Task<ChatDto?> CreateChatAsync(string ChatName)
        {
            try
            {
                _logger.LogInformation("Creating chat with name {Name}", ChatName);
                var CreateDto = new CreateChatDto { Id = ChatName, CreatedAt = DateTime.UtcNow };
                var createdChat = await _chatRepository.CreateAsync(CreateDto);
                return createdChat;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating chat with name {Name}",ChatName);
                return null;
            }
        }

        public async Task<Task?> DeleteChatAsync(string id)
        {
            try
            {
                _logger.LogInformation($"Deleting chat with Name: {id}");
                await _chatRepository.DeleteAsync(id);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting chat with Name: {id}");
                return null;

            }
        }

        public async Task<ICollection<ChatDto>?> GetAllChatsAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all chats");
                var chats = await _chatRepository.GetAllAsync();
                return chats.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all chats");
                return null;

            }
        }
    }
}


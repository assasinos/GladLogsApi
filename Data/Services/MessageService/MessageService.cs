using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Dtos.Message;
using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GladLogsApi.Data.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ICrudRepository<Guid, Message, MessageDto, CreateMessageDto> _messageCrudRepository;
        private readonly ICrudRepository<Guid, Message, ShortMessageDto, CreateMessageDto> _shortMessagecrudRepository;
        private readonly ILogger<MessageService> _logger;

        public MessageService(ICrudRepository<Guid, Message, MessageDto, CreateMessageDto> crudRepository, ILogger<MessageService> logger, ICrudRepository<Guid, Message, ShortMessageDto, CreateMessageDto> shortMessagecrudRepository)
        {
            _messageCrudRepository = crudRepository;
            _logger = logger;
            _shortMessagecrudRepository = shortMessagecrudRepository;
        }

        public MessageDto? CreateMessage(CreateMessageDto message)
        {
            try
            {
                _logger.LogInformation("Creating message with content {Content}", message.Content);
                var createdMessage = _messageCrudRepository.Create(message);
                return createdMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message with content {Content}", message.Content);
                return null;
            }
        }

        public async Task<MessageDto?> CreateMessageAsync(CreateMessageDto message)
        {
            try
            {
                _logger.LogInformation("Creating message with content {Content}", message.Content);
                var createdMessage = await _messageCrudRepository.CreateAsync(message);
                return createdMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message with content {Content}", message.Content);
                return null;
            }
        }

        public int? GetUserMessageCountByChat(string UserId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return _messageCrudRepository.GetQuery(q => q.Where(m => m.ChatId == ChatId && m.UserId == UserId)).Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return null;
            }
        }

        public async Task<int?> GetUserMessageCountByChatAsync(string UserId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return await _messageCrudRepository.GetQuery(q => q.Where(m => m.ChatId == ChatId && m.UserId == UserId)).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return null;
            }
        }

        public ICollection<MessageDto>? GetUserMessagesByChatAndWeek(string UserId, Guid WeekId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return _messageCrudRepository.GetQuery(q => q.Where(m => m.WeekId == WeekId && m.ChatId == ChatId && m.UserId == UserId)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return null;
            }
        }

        public async Task<ICollection<MessageDto>?> GetUserMessagesByChatAndWeekAsync(string UserId, Guid WeekId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return await _messageCrudRepository.GetQuery(q => q.Where(m => m.WeekId == WeekId && m.ChatId == ChatId && m.UserId == UserId)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return null;
            }
        }

        public ICollection<ShortMessageDto>? GetUserShortMessagesByChatAndWeek(string UserId, Guid WeekId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Short Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return _shortMessagecrudRepository.GetQuery(q => q.Where(m => m.WeekId == WeekId && m.ChatId == ChatId && m.UserId == UserId)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Short Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return null;
            }
        }

        public async Task<ICollection<ShortMessageDto>?> GetUserShortMessagesByChatAndWeekAsync(string UserId, Guid WeekId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Short Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return await _shortMessagecrudRepository.GetQuery(q => q.Where(m => m.WeekId == WeekId && m.ChatId == ChatId && m.UserId == UserId)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Short Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return null;
            }
        }
    }
}

﻿using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GladLogsApi.Data.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly ICrudRepository<Guid, Message, MessageDto, CreateMessageDto> _crudRepository;
        private readonly ILogger<MessageService> _logger;

        public MessageService(ICrudRepository<Guid, Message, MessageDto, CreateMessageDto> crudRepository, ILogger<MessageService> logger)
        {
            _crudRepository = crudRepository;
            _logger = logger;
        }

        public MessageDto? CreateMessage(CreateMessageDto message)
        {
            try
            {
                _logger.LogInformation("Creating message with content {Content}", message.Content);
                var createdMessage = _crudRepository.Create(message);
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
                var createdMessage = await _crudRepository.CreateAsync(message);
                return createdMessage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating message with content {Content}", message.Content);
                return null;
            }
        }

        public int? GetUserMessageCountByChat(Guid UserId, Guid ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return _crudRepository.GetQuery(q => q.Where(m => m.ChatId == ChatId && m.UserId == UserId)).Count();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return null;
            }
        }

        public async Task<int?> GetUserMessageCountByChatAsync(Guid UserId, Guid ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return await _crudRepository.GetQuery(q => q.Where(m => m.ChatId == ChatId && m.UserId == UserId)).CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Message Count of {User} in Chat {Chat}", UserId, ChatId);
                return null;
            }
        }

        public ICollection<MessageDto>? GetUserMessagesByChatAndWeek(Guid UserId, Guid WeekId, Guid ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return _crudRepository.GetQuery(q => q.Where(m => m.WeekId == WeekId && m.ChatId == ChatId && m.UserId == UserId)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return null;
            }
        }

        public async Task<ICollection<MessageDto>?> GetUserMessagesByChatAndWeekAsync(Guid UserId, Guid WeekId, Guid ChatId)
        {
            try
            {
                _logger.LogInformation("Getting Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return await _crudRepository.GetQuery(q => q.Where(m => m.WeekId == WeekId && m.ChatId == ChatId && m.UserId == UserId)).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Getting Messages of {User} in Week {Week}, with Chat: {Chat}", UserId, WeekId, ChatId);
                return null;
            }
        }
    }
}

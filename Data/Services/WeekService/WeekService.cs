using GladLogsApi.Data.Repositories.CrudRepository;
using GladLogsApi.Models.Dtos;
using GladLogsApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GladLogsApi.Data.Services.WeekService
{
    public class WeekService : IWeekService
    {

        private readonly ICrudRepository<Guid, Week, WeekDto, CreateWeekDto> _weekCrudRepository;


        private readonly ILogger<WeekService> _logger;

        public WeekService(ICrudRepository<Guid, Week, WeekDto, CreateWeekDto> weekCrudRepository, ILogger<WeekService> logger)
        {
            _weekCrudRepository = weekCrudRepository;
            _logger = logger;
        }

        public WeekDto? CreateWeek(CreateWeekDto createWeekDto)
        {
            try
            {
                _logger.LogInformation("Creating week with start date {StartDate} and end date {EndDate}", createWeekDto.StartDate, createWeekDto.EndDate);

                var week = _weekCrudRepository.Create(createWeekDto);

                return week;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating week with start date {StartDate} and end date {EndDate}", createWeekDto.StartDate, createWeekDto.EndDate);
                return null;
            }
        }

        public async Task<WeekDto?> CreateWeekAsync(CreateWeekDto createWeekDto)
        {
            try
            {
                _logger.LogInformation("Creating week with start date {StartDate} and end date {EndDate}", createWeekDto.StartDate, createWeekDto.EndDate);

                var week = await _weekCrudRepository.CreateAsync(createWeekDto);

                return week;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating week with start date {StartDate} and end date {EndDate}", createWeekDto.StartDate, createWeekDto.EndDate);
                return null;
            }
        }

        public IEnumerable <WeekDto>? GetAllWeeks()
        {
            try
            {
                _logger.LogInformation("Getting all weeks");

                var weeks = _weekCrudRepository.GetAll();

                return weeks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all weeks");
                return null;
            }
        }

        public async Task<IEnumerable<WeekDto>?> GetAllWeeksAsync()
        {
            try
            {
                _logger.LogInformation("Getting all weeks");

                var weeks =await _weekCrudRepository.GetAllAsync();

                return weeks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all weeks");
                return null;
            }
        }

        public WeekDto? GetCurrentWeek()
        {
            try
            {
                _logger.LogInformation("Getting current week");

                var week = _weekCrudRepository.GetQuery(q => q.Where(w => w.StartDate <= DateTime.Now && w.EndDate >= DateTime.Now)).FirstOrDefault();

                return week;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting current week");
                return null;
            }
        }

        public ICollection<WeekDto>? GetUserActiveWeeksInChat(string UserId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting active weeks for user {UserId} in chat {ChatId}", UserId, ChatId);

                var weeks = _weekCrudRepository.GetQuery(q => q.Where(w => w.Messages.Any(x => x.UserId == UserId && x.ChatId == ChatId)).Distinct()).ToList();

                return weeks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active weeks for user {UserId} in chat {ChatId}", UserId, ChatId);
                return null;
            }

        }

        public async Task<ICollection<WeekDto>?> GetUserActiveWeeksInChatAsync(string UserId, string ChatId)
        {
            try
            {
                _logger.LogInformation("Getting active weeks for user {UserId} in chat {ChatId}", UserId, ChatId);

                var weeks = await _weekCrudRepository.GetQuery(q => q.Where(w => w.Messages.Any(x => x.UserId == UserId && x.ChatId == ChatId)).Distinct()).ToListAsync();

                return weeks;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting active weeks for user {UserId} in chat {ChatId}", UserId, ChatId);
                return null;
            }
        }
    }
}

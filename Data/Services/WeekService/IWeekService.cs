using GladLogsApi.Models.Dtos;

namespace GladLogsApi.Data.Services.WeekService
{
    public interface IWeekService
    {
        ICollection<WeekDto>? GetUserActiveWeeksInChat(string UserId, string ChatId);
        Task<ICollection<WeekDto>?> GetUserActiveWeeksInChatAsync(string UserId, string ChatId);

        WeekDto? CreateWeek(CreateWeekDto createWeekDto);
        Task<WeekDto?> CreateWeekAsync(CreateWeekDto createWeekDto);

        IEnumerable<WeekDto>? GetAllWeeks();
        Task<IEnumerable<WeekDto>?> GetAllWeeksAsync();

    }
}

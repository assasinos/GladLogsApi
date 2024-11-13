namespace GladLogsApi.Models.Dtos
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string ChatId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid WeekId { get; set; }
    }
}

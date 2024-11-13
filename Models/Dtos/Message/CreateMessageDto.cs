namespace GladLogsApi.Models.Dtos
{
    public class CreateMessageDto
    {
        public string ChatId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid WeekId { get; set; }
    }
}

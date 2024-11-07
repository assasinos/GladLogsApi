namespace GladLogsApi.Models.Dtos
{
    public class CreateMessageDto
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid WeekId { get; set; }
    }
}

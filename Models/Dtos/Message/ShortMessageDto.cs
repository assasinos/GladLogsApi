namespace GladLogsApi.Models.Dtos.Message
{
    public class ShortMessageDto
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        public string Timestamp { get; set; } //Timestamp is a string because it is stored as a string in the database. The string is a ISO 8601 formatted date.

    }
}

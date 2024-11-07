using GladLogsApi.Models.Shared;

namespace GladLogsApi.Models.Entities
{

    /// <summary>
    /// Represents a chat or streaming session where messages are sent.
    /// </summary>
    public class Chat : EntityBase<Guid>
    {

        /// <summary>
        /// Name or identifier for the chat.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Date indicating when the chat was initially logged.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now.Date;

        /// <summary>
        /// Collection of messages associated with this chat.
        /// </summary>
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}

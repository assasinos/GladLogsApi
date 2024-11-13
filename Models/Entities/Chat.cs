using GladLogsApi.Models.Shared;

namespace GladLogsApi.Models.Entities
{

    /// <summary>
    /// Represents a chat or streaming session where messages are sent.
    /// </summary>
    public class Chat : EntityBase<string> // I belive that twitch usernames are unique, so I'm using them as the primary key.
    {

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

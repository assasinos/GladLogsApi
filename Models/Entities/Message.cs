using GladLogsApi.Models.Shared;
using System.IO;

namespace GladLogsApi.Models.Entities
{

    /// <summary>
    /// Represents an individual message sent in a chat.
    /// </summary>
    public class Message : EntityBase<Guid>
    {

        /// <summary>
        /// ID of the chat or stream where the message was sent.
        /// </summary>
        public Guid ChatId { get; set; }

        /// <summary>
        /// ID of the user who sent the message.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Text content of the message.
        /// </summary>
        public string Content { get; set; } = null!;

        /// <summary>
        /// Timestamp indicating when the message was sent.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// ID of the week during which the message was sent.
        /// </summary>
        public Guid WeekId { get; set; }

        // Navigation properties
        public Chat Chat { get; set; } = null!;
        public User User { get; set; } = null!;
        public Week Week { get; set; } = null!;
    }

}

using GladLogsApi.Models.Shared;

namespace GladLogsApi.Models.Entities
{


    /// <summary>
    /// Represents a user who can send messages in different chats.
    /// </summary>
    public class User : EntityBase<Guid>
    {

        /// <summary>
        /// Display name or username of the user.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Timestamp indicating when the user was initaly loged.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Collection of messages sent by this user.
        /// </summary>
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}

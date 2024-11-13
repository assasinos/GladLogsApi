using GladLogsApi.Models.Shared;

namespace GladLogsApi.Models.Entities
{


    /// <summary>
    /// Represents a user who can send messages in different chats.
    /// </summary>
    public class User : EntityBase<string> //I belive that twitch usernames are unique, so I'm using them as the primary key.
    {


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

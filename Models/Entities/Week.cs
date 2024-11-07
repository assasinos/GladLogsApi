using GladLogsApi.Models.Shared;

namespace GladLogsApi.Models.Entities
{
    /// <summary>
    /// Represents a weekly period for grouping messages.
    /// </summary>
    public class Week : EntityBase<Guid>
    {

        /// <summary>
        /// Start date of the week.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the week.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Collection of messages associated with this week.
        /// </summary>
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}

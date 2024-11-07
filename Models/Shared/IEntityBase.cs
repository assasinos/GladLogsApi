namespace GladLogsApi.Models.Shared
{
    public interface IEntityBase<TPrimaryKey>
    {
        /// <summary>
        /// Unique identifier for the entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}

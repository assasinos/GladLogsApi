namespace GladLogsApi.Models.Shared
{
    public class EntityBase<TPrimaryKey> :IEntityBase<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}

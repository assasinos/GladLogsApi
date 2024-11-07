
using GladLogsApi.Models.Shared;

namespace GladLogsApi.Data.Repositories.CrudRepository
{
    public class CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TCreateDto> : ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TCreateDto>
        where TEntityBase : class, IEntityBase<TPrimaryKey>, new()
    {


        /// <summary>
        /// DB context for the repository.
        /// </summary>
        private readonly ApplicationDbContext _context;

        private readonly ILogger<CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TCreateDto>> _logger;

        public CrudRepository(ApplicationDbContext context, ILogger<CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TCreateDto>> logger)
        {
            _context = context;
            _logger = logger;
        }
1        public TEntityDto Create(TCreateDto createDto)
        {
            try
            {
                var entity = _context.Set<TEntityBase>();
                _context.SaveChanges();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity.");
                throw;
            }

        }

        public void Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntityDto GetById(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntityBase>, IQueryable<TEntityBase>> queryBuilder)
        {
            throw new NotImplementedException();
        }

        public TEntityDto Update(TPrimaryKey id, TCreateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}

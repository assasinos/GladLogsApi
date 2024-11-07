
using AutoMapper;
using GladLogsApi.Models.Shared;
using Microsoft.EntityFrameworkCore;

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

        private readonly IMapper _mapper;

        public CrudRepository(ApplicationDbContext context, ILogger<CrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TCreateDto>> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }
        public TEntityDto Create(TCreateDto createDto)
        {
            try
            {
                var entity = _context.Set<TEntityBase>();
                _context.SaveChanges();
                return _mapper.Map<TEntityDto>(entity);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error occurred.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }

        }

        public void Delete(TPrimaryKey id)
        {
            try
            {
                var entity = _context.Set<TEntityBase>().Find(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                _context.Set<TEntityBase>().Remove(entity);
                _context.SaveChanges();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error occurred.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            try
            {
                var entities = _context.Set<TEntityBase>().ToList();
                return _mapper.Map<IEnumerable<TEntityDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

        public TEntityDto GetById(TPrimaryKey id)
        {
            try
            {
                var entity = _context.Set<TEntityBase>().Find(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                return _mapper.Map<TEntityDto>(entity);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

        public IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntityBase>, IQueryable<TEntityBase>> queryBuilder)
        {
            try
            {
                var query = queryBuilder(_context.Set<TEntityBase>());
                return _mapper.ProjectTo<TEntityDto>(query);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

        public TEntityDto Update(TPrimaryKey id, TCreateDto updateDto)
        {
            try
            {
                var entity = _context.Set<TEntityBase>().Find(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                _mapper.Map(updateDto, entity);
                _context.SaveChanges();
                return _mapper.Map<TEntityDto>(entity);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency error occurred.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating the database.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }
    }
}

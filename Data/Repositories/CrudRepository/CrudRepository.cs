
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
                var entityContext = _context.Set<TEntityBase>();
                var createEntity = _mapper.Map<TEntityBase>(createDto);
                var entity =  entityContext.Add(createEntity);
                _context.SaveChanges();
                var entityDto = _mapper.Map<TEntityDto>(entity.Entity);
                if (entityDto is null) {
                    throw new Exception("Error creating entity.");
                }

                return entityDto;
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

        public async Task<TEntityDto> CreateAsync(TCreateDto createDto)
        {
            try
            {
                var entityContext = _context.Set<TEntityBase>();
                var createEntity = _mapper.Map<TEntityBase>(createDto);
                var entity = await entityContext.AddAsync(createEntity);
                await _context.SaveChangesAsync();
                var entityDto = _mapper.Map<TEntityDto>(entity.Entity);

                return entityDto is null ? throw new Exception("Error creating entity.") : entityDto;
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

        public async Task DeleteAsync(TPrimaryKey id)
        {
            try
            {
                var entity = await _context.Set<TEntityBase>().FindAsync(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                _context.Set<TEntityBase>().Remove(entity);
                await _context.SaveChangesAsync();
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
                var entityContext = _context.Set<TEntityBase>();
                var entities = entityContext.ToList();
                return _mapper.Map<IEnumerable<TEntityDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw;
            }
        }

        public async Task<IEnumerable<TEntityDto>> GetAllAsync()
        {
            try
            {
                var entityContext = _context.Set<TEntityBase>();
                var entities = await entityContext.ToListAsync();
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
                var entityContext = _context.Set<TEntityBase>();

                var entity = entityContext.Find(id);

                if (entity is null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                var entityDto = _mapper.Map<TEntityDto>(entity);

                return entityDto;
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

        public async Task<TEntityDto> GetByIdAsync(TPrimaryKey id)
        {
            try
            {
                var entityContext = _context.Set<TEntityBase>();

                var entity = await entityContext.FindAsync(id);
                if (entity is null)
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
                var entityContext = _context.Set<TEntityBase>();
                var entity = entityContext.Find(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                var CreateEntity = _mapper.Map<TEntityBase>(updateDto);

                if (CreateEntity is null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} couldn't be converted");
                }

                var updatedEntity =  entityContext.Update(CreateEntity);

                _context.SaveChanges();

                var entityDto = _mapper.Map<TEntityDto>(updatedEntity.Entity);

                if (entityDto is null)
                {
                    throw new Exception("Error updating entity.");
                }

                return entityDto;
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

        public async Task<TEntityDto> UpdateAsync(TPrimaryKey id, TCreateDto updateDto)
        {
            try
            {
                var entityContext = _context.Set<TEntityBase>();
                var entity = await entityContext.FindAsync(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} not found.");
                }
                var CreateEntity = _mapper.Map<TEntityBase>(updateDto);

                if (CreateEntity is null)
                {
                    throw new KeyNotFoundException($"Entity {typeof(TEntityBase)} with id {id} couldn't be converted");
                }

                var updatedEntity = entityContext.Update(CreateEntity);

                _context.SaveChanges();

                var entityDto = _mapper.Map<TEntityDto>(updatedEntity.Entity);

                if (entityDto is null)
                {
                    throw new Exception("Error updating entity.");
                }

                return entityDto;
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

namespace GladLogsApi.Data.Repositories.CrudRepository
{

    /// <summary>
    /// Represents a repository that provides basic CRUD operations for an entity.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key of the entity.</typeparam>
    /// <typeparam name="TEntityBase">The base type of the entity.</typeparam>
    /// <typeparam name="TEntityDto">The type of the entity DTO.</typeparam>
    /// <typeparam name="TCreateDto">The type of the DTO used to create or update the entity.</typeparam>
    public interface ICrudRepository<TPrimaryKey, TEntityBase, TEntityDto, TCreateDto>
    {
        #region Basic CRUD
        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity.</param>
        /// <returns>The entity DTO.</returns>
        TEntityDto GetById(TPrimaryKey id);
        /// <summary>
        /// Retrieves all entities.
        /// </summary>
        /// <returns>A collection of entity DTOs.</returns>
        IEnumerable<TEntityDto> GetAll();
        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="createDto">The DTO containing the data to create the entity.</param>
        /// <returns>The created entity DTO.</returns>
        TEntityDto Create(TCreateDto createDto);
        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The primary key of the entity to update.</param>
        /// <param name="updateDto">The DTO containing the updated data.</param>
        /// <returns>The updated entity DTO.</returns>
        TEntityDto Update(TPrimaryKey id, TCreateDto updateDto);
        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity to delete.</param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// Asynchronously retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity DTO.</returns>
        Task<TEntityDto> GetByIdAsync(TPrimaryKey id);
        /// <summary>
        /// Asynchronously retrieves all entities.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of entity DTOs.</returns>
        Task<IEnumerable<TEntityDto>> GetAllAsync();
        /// <summary>
        /// Asynchronously creates a new entity.
        /// </summary>
        /// <param name="createDto">The DTO containing the data to create the entity.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created entity DTO.</returns>
        Task<TEntityDto> CreateAsync(TCreateDto createDto);
        /// <summary>
        /// Asynchronously updates an existing entity.
        /// </summary>
        /// <param name="id">The primary key of the entity to update.</param>
        /// <param name="updateDto">The DTO containing the updated data.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity DTO.</returns>
        Task<TEntityDto> UpdateAsync(TPrimaryKey id, TCreateDto updateDto);
        /// <summary>
        /// Asynchronously deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteAsync(TPrimaryKey id);
        #endregion


        #region Advanced CRUD
        /// <summary>
        /// Retrieves a queryable collection of entities based on a query builder function.
        /// </summary>
        /// <param name="queryBuilder">A function to build the query.</param>
        /// <returns>A queryable collection of entity DTOs.</returns>
        IQueryable<TEntityDto> GetQuery(Func<IQueryable<TEntityBase>, IQueryable<TEntityBase>> queryBuilder);
        #endregion

    }
}

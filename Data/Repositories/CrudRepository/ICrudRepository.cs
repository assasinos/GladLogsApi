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

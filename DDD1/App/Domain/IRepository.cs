// <copyright file="IRepository.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Core.Domain
{
    using System.Threading.Tasks;

    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="T">An IEntity type.</typeparam>
    public interface IRepository<T>
        where T : IEntity
    {
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns>ID of new entity.</returns>
        Task<int> AddAsync(T entity);

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">ID of stock to delete.</param>
        /// <returns>ID if deleted.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <returns>True if updated.</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Find an entity by ID.
        /// </summary>
        /// <param name="id">ID of entity to get.</param>
        /// <returns>The entry.  Null if not found.</returns>
        Task<T> FindByIdAsync(int id);
    }
}
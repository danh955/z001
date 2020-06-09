// <copyright file="IRepository.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Core.Storage
{
    using System;
    using System.Threading.Tasks;
    using DDD1.Core.Domain;

    /// <summary>
    /// Repository interface.
    /// </summary>
    /// <typeparam name="T">An IEntity type.</typeparam>
    /// <typeparam name="TId">IEntity ID type.</typeparam>
    public interface IRepository<T, TId>
        where T : IAggregateRoot<TId>
        where TId : struct
    {
        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        /// <returns>ID of new entity.</returns>
        Task<TId> AddAsync(T entity);

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">ID of stock to delete.</param>
        /// <returns>True if deleted.</returns>
        Task<bool> DeleteAsync(TId id);

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
        Task<T> FindByIdAsync(TId id);
    }
}
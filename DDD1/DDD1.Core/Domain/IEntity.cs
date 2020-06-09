// <copyright file="IEntity.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Core.Domain
{
    /// <summary>
    /// Entity interface.
    /// A child entity of the root aggregate entity.
    /// </summary>
    /// <typeparam name="TId">ID type.</typeparam>
    public interface IEntity<TId>
        where TId : struct
    {
        /// <summary>
        /// Gets or sets ID of the record.
        /// </summary>
        public TId Id { get; set; }
    }
}
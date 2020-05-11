// <copyright file="IEntity.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace LiteDbLoader.Model
{
    /// <summary>
    /// Entity interface.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Gets or sets ID of the record.
        /// </summary>
        public int Id { get; set; }
    }
}
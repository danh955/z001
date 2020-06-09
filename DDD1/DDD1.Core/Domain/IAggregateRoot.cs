// <copyright file="IAggregateRoot.cs" company="Hilres">
// Copyright (c) Hilres. All rights reserved.
// </copyright>

namespace DDD1.Core.Domain
{
    /// <summary>
    /// Aggregate root interface.
    /// The root entity that may or may no have children entities.
    /// </summary>
    /// <typeparam name="TId">IEntity ID type.</typeparam>
    public interface IAggregateRoot<TId> : IEntity<TId>
        where TId : struct
    {
    }
}
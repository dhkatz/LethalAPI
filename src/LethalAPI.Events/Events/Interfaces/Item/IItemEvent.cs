// -----------------------------------------------------------------------
// <copyright file="IItemEvent.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Interfaces.Item;

using LethalAPI.API.Features.Items;

/// <summary>
/// Interface for all <see cref="Item"/> related events.
/// </summary>
public interface IItemEvent : IEvent
{
    /// <summary>
    /// Gets the <see cref="Item"/> related to the event.
    /// </summary>
    public Item Item { get; }
}

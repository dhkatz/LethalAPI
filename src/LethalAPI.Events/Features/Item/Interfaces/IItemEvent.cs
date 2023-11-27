// -----------------------------------------------------------------------
// <copyright file="IItemEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Item.Interfaces;

using API.Features.Items;
using LethalAPI.Events.Interfaces;

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

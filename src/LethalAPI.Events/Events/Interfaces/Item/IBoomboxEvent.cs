// -----------------------------------------------------------------------
// <copyright file="IBoomboxEvent.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Interfaces.Item;

using LethalAPI.API.Features.Items;

/// <summary>
/// Event arguments associated with a <see cref="Boombox"/> related event.
/// </summary>
public interface IBoomboxEvent : IItemEvent
{
    /// <summary>
    /// Gets the <see cref="Boombox"/> associated with the event.
    /// </summary>
    public Boombox Boombox { get; }
}

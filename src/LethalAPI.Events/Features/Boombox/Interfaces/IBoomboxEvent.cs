// -----------------------------------------------------------------------
// <copyright file="IBoomboxEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Boombox.Interfaces;

using API.Features.Items;
using Item.Interfaces;

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

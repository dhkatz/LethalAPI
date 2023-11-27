// -----------------------------------------------------------------------
// <copyright file="IPlayerEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Player.Interfaces;

using API.Features;
using LethalAPI.Events.Interfaces;

/// <summary>
/// Interface for all <see cref="Player"/> related events.
/// </summary>
public interface IPlayerEvent : IEvent
{
    /// <summary>
    /// Gets or sets the <see cref="Player"/> that is related to the event.
    /// </summary>
    public Player Player { get; set; }
}

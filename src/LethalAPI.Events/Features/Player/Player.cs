// -----------------------------------------------------------------------
// <copyright file="Player.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Player;

using Events;
using LethalAPI.Events.Events;

/// <summary>
/// Event handlers related to the player.
/// </summary>
public static class Player
{
    /// <summary>
    /// Called when the player starts emoting.
    /// </summary>
    public static Event<PlayerEmoteStartEvent> PlayerEmoteStart { get; set; } = new ();

    /// <summary>
    /// Called when the player stops emoting.
    /// </summary>
    public static Event<PlayerEmoteStopEvent> PlayerEmoteStop { get; set; } = new ();
}

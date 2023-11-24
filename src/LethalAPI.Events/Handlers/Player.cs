// -----------------------------------------------------------------------
// <copyright file="Player.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Handlers;

using Events.Player;
using Features;

/// <summary>
/// Event handlers related to the player.
/// </summary>
public static class Player
{
    /// <summary>
    /// Called when the player starts emoting.
    /// </summary>
    public static Event<PlayerEmoteStartEventArgs> PlayerEmoteStart { get; set; } = new ();

    /// <summary>
    /// Called when the player stops emoting.
    /// </summary>
    public static Event<PlayerEmoteStopEventArgs> PlayerEmoteStop { get; set; } = new ();
}

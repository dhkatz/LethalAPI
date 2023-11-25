// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStopEvent.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Player;

extern alias LethalCompany;

using API.Features;
using Interfaces;
using JetBrains.Annotations;
using LethalCompany::GameNetcodeStuff;

[UsedImplicitly]
public record PlayerEmoteStopEvent : IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEmoteStopEvent"/> class.
    /// </summary>
    /// <param name="playerControllerB">The <see cref="PlayerControllerB"/> instance.</param>
    public PlayerEmoteStopEvent(PlayerControllerB playerControllerB)
    {
        Player = Player.Get(playerControllerB);
    }

    /// <inheritdoc />
    public Player Player { get; set; }
}

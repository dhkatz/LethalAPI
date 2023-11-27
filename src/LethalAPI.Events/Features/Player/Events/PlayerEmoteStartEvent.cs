// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStartEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Player.Events;

extern alias LethalCompany;
using API.Features;
using Interfaces;
using JetBrains.Annotations;
using PlayerControllerB = LethalCompany::GameNetcodeStuff.PlayerControllerB;

[UsedImplicitly]
public sealed class PlayerEmoteStartEvent : IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEmoteStartEvent"/> class.
    /// </summary>
    /// <param name="playerControllerB">The <see cref="GameNetcodeStuff.PlayerControllerB"/> instance.</param>
    public PlayerEmoteStartEvent(PlayerControllerB playerControllerB)
    {
        Player = Player.Get(playerControllerB);
    }

    /// <inheritdoc cref="IPlayerEvent.Player"/>
    public Player Player { get; set; }
}

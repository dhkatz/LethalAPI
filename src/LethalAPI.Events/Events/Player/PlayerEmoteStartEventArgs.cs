// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStartEventArgs.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Player;

extern alias LethalCompany;

using JetBrains.Annotations;
using LethalCompany::GameNetcodeStuff;
using LethalAPI.API.Features;
using Interfaces;

[UsedImplicitly]
public record PlayerEmoteStartEventArgs : IPlayerEvent
{
    public PlayerEmoteStartEventArgs(PlayerControllerB playerControllerB)
    {
        Player = Player.Get(playerControllerB);
    }

    /// <inheritdoc cref="IPlayerEvent.Player"/>
    public Player Player { get; set; }
}

// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStopEventArgs.cs" company="LethalLib">
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
public record PlayerEmoteStopEventArgs : IPlayerEvent
{
    public PlayerEmoteStopEventArgs(PlayerControllerB playerControllerB)
    {
        Player = Player.Get(playerControllerB);
    }

    public Player Player { get; set; }
}

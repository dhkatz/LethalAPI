// -----------------------------------------------------------------------
// <copyright file="Boombox.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Boombox;

using Events;
using JetBrains.Annotations;
using LethalAPI.Events.Events;

/// <summary>
/// Event handlers related to the boombox.
/// </summary>
public static class Boombox
{
    /// <summary>
    /// Called when the boombox starts playing.
    /// </summary>
    [UsedImplicitly]
    public static Event<BoomboxStartEvent> BoomboxStart { get; set; } = new ();

    /// <summary>
    /// Called when the boombox stops playing.
    /// </summary>
    [UsedImplicitly]
    public static Event<BoomboxStopEvent> BoomboxStop { get; set; } = new ();
}

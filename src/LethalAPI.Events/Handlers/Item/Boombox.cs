// -----------------------------------------------------------------------
// <copyright file="Boombox.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Handlers.Item;

using Events.Item.Boombox;
using JetBrains.Annotations;
using Features;

/// <summary>
/// Event handlers related to the boombox.
/// </summary>
public static class Boombox
{
    /// <summary>
    /// Called when the boombox starts playing.
    /// </summary>
    [UsedImplicitly]
    public static Event<BoomboxStartEventArgs> BoomboxStart { get; set; } = new ();

    /// <summary>
    /// Called when the boombox stops playing.
    /// </summary>
    [UsedImplicitly]
    public static Event<BoomboxStopEventArgs> BoomboxStop { get; set; } = new ();
}

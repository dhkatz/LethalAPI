// -----------------------------------------------------------------------
// <copyright file="Item.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Handlers.Item;

using Events.Item;
using Features;

/// <summary>
/// Event handlers related to items.
/// </summary>
public static class Item
{
    /// <summary>
    /// Called when an item is picked up.
    /// </summary>
    public static Event<ItemPickupEventArgs> ItemPickup { get; set; } = new ();

    /// <summary>
    /// Called when an item is dropped.
    /// </summary>
    public static Event<ItemDropEventArgs> ItemDrop { get; set; } = new ();
}

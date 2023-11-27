// -----------------------------------------------------------------------
// <copyright file="Item.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Item;

using Events;
using Features;
using LethalAPI.Events.Events;

/// <summary>
/// Event handlers related to items.
/// </summary>
public static class Item
{
    /// <summary>
    /// Called when an item is picked up.
    /// </summary>
    public static Event<ItemPickupEvent> ItemPickup { get; set; } = new ();

    /// <summary>
    /// Called when an item is dropped.
    /// </summary>
    public static Event<ItemDropEvent> ItemDrop { get; set; } = new ();
}

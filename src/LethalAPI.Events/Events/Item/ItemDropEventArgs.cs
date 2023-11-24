// -----------------------------------------------------------------------
// <copyright file="ItemDropEventArgs.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Item;

extern alias LethalCompany;
using System;
using LethalAPI.API.Features.Items;
using Interfaces.Item;
using GrabbableObject = LethalCompany::GrabbableObject;

public sealed class ItemDropEventArgs : IItemEvent
{
    public ItemDropEventArgs(GrabbableObject grabbableObject)
    {
        var item = Item.Get(grabbableObject);

        Item = item ?? throw new NullReferenceException("Tried to create ItemDropEventArgs with non-item!");
    }

    /// <inheritdoc />
    public Item Item { get; }
}

// -----------------------------------------------------------------------
// <copyright file="ItemDropEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Item.Events;

extern alias LethalCompany;
using System;
using API.Features.Items;
using Interfaces;
using GrabbableObject = LethalCompany::GrabbableObject;

public sealed class ItemDropEvent : IItemEvent
{
    public ItemDropEvent(GrabbableObject grabbableObject)
    {
        var item = Item.Get(grabbableObject);

        Item = item ?? throw new NullReferenceException("Tried to create ItemDropEvent with non-item!");
    }

    /// <inheritdoc />
    public Item Item { get; }
}

// -----------------------------------------------------------------------
// <copyright file="BoomboxStartEvent.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Item.Boombox;

extern alias LethalCompany;
using System;
using LethalAPI.API.Features.Items;
using Interfaces.Item;
using JetBrains.Annotations;
using BoomboxItem = LethalCompany::BoomboxItem;

[UsedImplicitly]
public sealed class BoomboxStartEvent : IBoomboxEvent, IDeniableEvent
{
    public BoomboxStartEvent(BoomboxItem boomboxItem)
    {
        var item = Item.Get(boomboxItem);

        if (item is not Boombox boombox)
        {
            throw new NullReferenceException("Tried to create BoomboxStartEvent with non-boombox!");
        }

        Boombox = boombox;
    }

    /// <inheritdoc />
    public Boombox Boombox { get; }

    /// <inheritdoc />
    public Item Item => Boombox;

    /// <inheritdoc />
    public bool IsAllowed { get; set; } = true;
}

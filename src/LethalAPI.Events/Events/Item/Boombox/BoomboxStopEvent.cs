// -----------------------------------------------------------------------
// <copyright file="BoomboxStopEvent.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events.Item.Boombox;

extern alias LethalCompany;
using System;
using LethalAPI.API.Features.Items;
using Interfaces.Item;
using BoomboxItem = LethalCompany::BoomboxItem;

public sealed class BoomboxStopEvent : IBoomboxEvent
{
    public BoomboxStopEvent(BoomboxItem boomboxItem)
    {
        var item = Item.Get(boomboxItem);

        if (item is not Boombox boombox)
        {
            throw new NullReferenceException("Tried to create BoomboxStopEvent with non-boombox item!");
        }

        Boombox = boombox;
    }

    /// <inheritdoc />
    public Boombox Boombox { get; }

    /// <inheritdoc />
    public Item Item => Boombox;
}

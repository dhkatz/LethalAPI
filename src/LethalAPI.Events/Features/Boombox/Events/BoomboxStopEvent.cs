// -----------------------------------------------------------------------
// <copyright file="BoomboxStopEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Boombox.Events;

extern alias LethalCompany;
using System;
using Interfaces;
using LethalAPI.API.Features.Items;
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

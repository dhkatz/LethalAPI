// -----------------------------------------------------------------------
// <copyright file="BoomboxStartEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Boombox.Events;

extern alias LethalCompany;
using System;
using API.Features.Items;
using Interfaces;
using JetBrains.Annotations;
using LethalAPI.Events.Interfaces;
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

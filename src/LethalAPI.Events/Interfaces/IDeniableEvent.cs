// -----------------------------------------------------------------------
// <copyright file="IDeniableEvent.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Interfaces;

/// <summary>
/// An event that can be denied or cancelled.
/// </summary>
public interface IDeniableEvent : IEvent
{
    /// <summary>
    /// Gets or sets a value indicating whether the event is allowed to continue.
    /// </summary>
    public bool IsAllowed { get; set; }
}

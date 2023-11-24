// -----------------------------------------------------------------------
// <copyright file="IDeniableEvent.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events;

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

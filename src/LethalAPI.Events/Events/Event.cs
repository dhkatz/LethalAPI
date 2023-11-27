// -----------------------------------------------------------------------
// <copyright file="Event.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Events;

using System;
using Core;
using Interfaces;
using JetBrains.Annotations;

/// <summary>
/// Wrapper for a generic event.
/// </summary>
/// <typeparam name="T">The type of the event arguments.</typeparam>
public class Event<T> : IEvent
{
    private event Action<T> Handler = _ => { };

    public static Event<T> operator +(Event<T> @event, Action<T> handler)
    {
        @event.Subscribe(handler);
        return @event;
    }

    public static Event<T> operator -(Event<T> @event, Action<T> handler)
    {
        @event.Unsubscribe(handler);
        return @event;
    }

    /// <summary>
    /// Subscribes to the event.
    /// </summary>
    /// <param name="handler">The handler.</param>
    [UsedImplicitly]
    public void Subscribe(Action<T> handler)
    {
        Log.Debug("Subscribing to event {0}", typeof(T).Name);
        Handler += handler;
    }

    /// <summary>
    /// Unsubscribes from the event.
    /// </summary>
    /// <param name="handler">The handler.</param>
    [UsedImplicitly]
    public void Unsubscribe(Action<T> handler)
    {
        Log.Debug("Unsubscribing from event {0}", typeof(T).Name);
        Handler -= handler;
    }

    /// <summary>
    /// Safely invokes the event, catching any exceptions.
    /// This prevents the event from breaking patched code.
    /// </summary>
    /// <param name="eventArgs">The event arguments.</param>
    public void InvokeSafely(T eventArgs)
    {
        try
        {
            Handler.Invoke(eventArgs);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
        }
    }
}

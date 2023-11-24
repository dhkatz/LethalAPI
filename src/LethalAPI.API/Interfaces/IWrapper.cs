// -----------------------------------------------------------------------
// <copyright file="IWrapper.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.API.Interfaces;

using UnityEngine;

/// <summary>
/// Defines the contract for classes that wrap a base game object.
/// </summary>
/// <typeparam name="T">The wrapped base game class.</typeparam>
public interface IWrapper<out T>
    where T : MonoBehaviour
{
    /// <summary>
    /// Gets the base <see cref="MonoBehaviour"/> object.
    /// </summary>
    public T Base { get; }
}

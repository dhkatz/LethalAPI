// -----------------------------------------------------------------------
// <copyright file="Hazard.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.API.Features.Hazard;

using Interfaces;
using UnityEngine;

public class Hazard<T> : IWrapper<T>
    where T : MonoBehaviour
{
    public Hazard(T hazard)
    {
        Base = hazard;
    }

    /// <inheritdoc/>
    public T Base { get; }
}

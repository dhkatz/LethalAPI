// -----------------------------------------------------------------------
// <copyright file="Enemy.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.API.Features.Enemy;

extern alias LethalCompany;
using Interfaces;
using EnemyAI = LethalCompany::EnemyAI;

/// <summary>
/// A wrapper for the <see cref="EnemyAI"/> class.
/// </summary>
public class Enemy : IWrapper<EnemyAI>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Enemy"/> class.
    /// </summary>
    /// <param name="enemy">The <see cref="EnemyAI"/> being wrapped.</param>
    public Enemy(EnemyAI enemy)
    {
        Base = enemy;
    }

    /// <inheritdoc/>
    public EnemyAI Base { get; }

    /// <summary>
    /// Gets the name of the enemy.
    /// </summary>
    public string Name => Base.enemyType.enemyName;

    /// <summary>
    /// Gets the health of the enemy.
    /// </summary>
    public int Health => Base.enemyHP;
}

extern alias LethalCompany;

namespace LethalAPI.API.Features;

using System.Collections.Generic;
using System.Linq;
using Interfaces;
using LethalCompany::GameNetcodeStuff;
using UnityEngine;
using CauseOfDeath = LethalCompany::CauseOfDeath;
using StartOfRound = LethalCompany::StartOfRound;

/// <summary>
/// A wrapper class for <see cref="PlayerControllerB"/>.
/// You should interact with players using this class.
/// </summary>
public class Player : IWrapper<PlayerControllerB>
{
    public static Dictionary<GameObject, Player> List = new ();

    public Player(PlayerControllerB player)
    {
        Base = player;
    }

    public PlayerControllerB Base { get; }

    /// <summary>
    /// Gets a value indicating whether or not the client is the owner of the player.
    /// </summary>
    public bool IsOwner => Base.IsOwner;

    /// <summary>
    /// Gets a value indicating whether or not the player is alive.
    /// </summary>
    public bool IsAlive => !Base.isPlayerDead;

    public static Player Get(PlayerControllerB player)
    {
        return List.TryGetValue(player.gameObject, out var p) ? p : new Player(player);
    }

    public static IEnumerable<Player> GetAll()
    {
        return StartOfRound.Instance.allPlayerScripts.Select(Get);
    }

    public void Emote()
    {
    }

    /// <summary>
    /// Damages the player.
    /// </summary>
    /// <param name="damage">Damage to deal.</param>
    /// <param name="causeOfDeath">The cause of death, if killed by this damage.</param>
    public void Damage(int damage, CauseOfDeath causeOfDeath)
    {
        Base.DamagePlayer(damage, causeOfDeath: causeOfDeath);
    }

    /// <summary>
    /// Kills the player.
    /// </summary>
    /// <param name="causeOfDeath">The cause of death.</param>
    public void Kill(CauseOfDeath causeOfDeath)
    {
        Base.KillPlayer(Vector3.zero, causeOfDeath: causeOfDeath);
    }
}

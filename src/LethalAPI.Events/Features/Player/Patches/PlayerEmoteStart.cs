// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStart.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Player.Patches;

extern alias LethalCompany;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Events;
using Extensions;
using HarmonyLib;
using JetBrains.Annotations;
using LethalCompany::GameNetcodeStuff;

/// <summary>
/// Patches <see cref="GameNetcodeStuff.PlayerControllerB.PerformEmote"/>
/// </summary>
[HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.PerformEmote))]
internal static class PlayerEmoteStart
{
    private static readonly FieldInfo TimeSinceStartingEmoteFieldInfo =
        AccessTools.Field(typeof(PlayerControllerB), nameof(PlayerControllerB.timeSinceStartingEmote));

    [UsedImplicitly]
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase original)
    {
        return new CodeMatcher(instructions, generator)
            .SearchForward(i => i.StoresField(TimeSinceStartingEmoteFieldInfo))
            .Advance(-2)
            .InsertEvent<PlayerEmoteStartEvent>(original)
            .InstructionEnumeration();
    }
}

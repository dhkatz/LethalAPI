// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStart.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Patches.Player;

extern alias LethalCompany;

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Events.Player;
using HarmonyLib;
using Extensions;
using JetBrains.Annotations;

/// <summary>
/// Patches <see cref="GameNetcodeStuff.PlayerControllerB.PerformEmote"/>
/// </summary>
[HarmonyPatch(typeof(LethalCompany::GameNetcodeStuff.PlayerControllerB), nameof(LethalCompany::GameNetcodeStuff.PlayerControllerB.PerformEmote))]
internal static class PlayerEmoteStart
{
    private static readonly FieldInfo TimeSinceStartingEmoteFieldInfo =
        AccessTools.Field(typeof(LethalCompany::GameNetcodeStuff.PlayerControllerB), nameof(LethalCompany::GameNetcodeStuff.PlayerControllerB.timeSinceStartingEmote));

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

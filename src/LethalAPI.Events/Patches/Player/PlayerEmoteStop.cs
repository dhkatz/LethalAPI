// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStop.cs" company="LethalLib">
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
using Extensions;
using HarmonyLib;
using JetBrains.Annotations;
using LethalCompany::GameNetcodeStuff;

/// <summary>
/// Patches <see cref="PlayerControllerB.Update"/>
/// </summary>
[HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.Update))]
internal class PlayerEmoteStop
{
    private static readonly FieldInfo PerformingEmoteFieldInfo =
        AccessTools.Field(typeof(PlayerControllerB), nameof(PlayerControllerB.performingEmote));

    [UsedImplicitly]
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase original)
    {
        return new CodeMatcher(instructions, generator)
            .SearchForward(i => i.StoresField(PerformingEmoteFieldInfo))
            .Advance(-2)
            .InsertEvent<PlayerEmoteStopEventArgs>(original)
            .InstructionEnumeration();
    }
}

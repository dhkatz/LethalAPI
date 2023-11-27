// -----------------------------------------------------------------------
// <copyright file="PlayerEmoteStop.cs" company="LethalAPI">
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
using PlayerControllerB = LethalCompany::GameNetcodeStuff.PlayerControllerB;

/// <summary>
/// Patches <see cref="GameNetcodeStuff.PlayerControllerB.Update"/>
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
            .InsertEvent<PlayerEmoteStopEvent>(original)
            .InstructionEnumeration();
    }
}

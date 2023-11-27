// -----------------------------------------------------------------------
// <copyright file="BoomboxStop.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Features.Boombox.Patches;

extern alias LethalCompany;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Events;
using Extensions;
using HarmonyLib;
using JetBrains.Annotations;
using static HarmonyLib.AccessTools;
using BoomboxItem = LethalCompany::BoomboxItem;

/// <summary>
/// Patches <see cref="LethalCompany::BoomboxItem.StartMusic"/> to invoke <see cref="Boombox.BoomboxStart"/>.
/// </summary>
[HarmonyPatch(typeof(BoomboxItem), "StartMusic")]
internal static class BoomboxStop
{
    private static readonly FieldInfo IsPlayingMusicFieldInfo =
        Field(typeof(BoomboxItem), nameof(BoomboxItem.isPlayingMusic));

    /// <summary>
    /// Transpiles <see cref="LethalCompany::BoomboxItem.StartMusic"/> to invoke <see cref="Boombox.BoomboxStart"/>.
    /// </summary>
    /// <param name="instructions"></param>
    /// <param name="generator"></param>
    /// <param name="original"></param>
    /// <returns></returns>
    [UsedImplicitly]
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase original)
    {
        return new CodeMatcher(instructions, generator)
            .SearchForward(i => i.LoadsField(IsPlayingMusicFieldInfo))
            .Advance(2)
            .InsertEvent<BoomboxStopEvent>(original)
            .InstructionEnumeration();
    }
}

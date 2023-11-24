// -----------------------------------------------------------------------
// <copyright file="BoomboxStop.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events.Patches.Item.Boombox;

extern alias LethalCompany;

using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Events.Item.Boombox;
using Extensions;
using HarmonyLib;
using JetBrains.Annotations;
using static HarmonyLib.AccessTools;

/// <summary>
/// Patches <see cref="LethalCompany::BoomboxItem.StartMusic"/> to invoke <see cref="Handlers.Item.Boombox.BoomboxStart"/>.
/// </summary>
[HarmonyPatch(typeof(LethalCompany::BoomboxItem), "StartMusic")]
internal static class BoomboxStop
{
    private static readonly FieldInfo IsPlayingMusicFieldInfo =
        Field(typeof(LethalCompany::BoomboxItem), nameof(LethalCompany::BoomboxItem.isPlayingMusic));

    /// <summary>
    /// Transpiles <see cref="LethalCompany::BoomboxItem.StartMusic"/> to invoke <see cref="Handlers.Item.Boombox.BoomboxStart"/>.
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
            .InsertEvent<BoomboxStopEventArgs>(original)
            .InstructionEnumeration();
    }
}

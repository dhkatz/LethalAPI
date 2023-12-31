// -----------------------------------------------------------------------
// <copyright file="BoomboxStart.cs" company="LethalAPI">
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
using UnityEngine;
using static HarmonyLib.AccessTools;
using BoomboxItem = LethalCompany::BoomboxItem;

/// <summary>
/// Patches <see cref="LethalCompany::BoomboxItem.StartMusic"/> to invoke <see cref="Boombox.BoomboxStart"/>.
/// </summary>
[HarmonyPatch(typeof(BoomboxItem), nameof(BoomboxItem.StartMusic))]
internal static class BoomboxStart
{
    private static readonly MethodInfo AudioSourcePlayMethodInfo =
        Method(typeof(AudioSource), nameof(AudioSource.Play));

    [UsedImplicitly]
    internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator, MethodBase original)
    {
        return new CodeMatcher(instructions, generator)
            .SearchForward(i => i.Calls(AudioSourcePlayMethodInfo))
            .Advance(-2)
            .InsertDeniableEvent<BoomboxStartEvent>(original)
            .InstructionEnumeration();
    }
}

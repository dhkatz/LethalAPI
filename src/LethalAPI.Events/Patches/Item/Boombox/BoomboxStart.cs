// -----------------------------------------------------------------------
// <copyright file="BoomboxStart.cs" company="LethalLib">
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
using Handlers.Item;
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
            .InsertDeniableEvent<BoomboxStartEventArgs>(original)
            .InstructionEnumeration();
    }
}

namespace LethalAPI.Core.Patches.Items;

extern alias LethalCompany;
using System.Collections.Generic;
using BepInEx.Logging;
using HarmonyLib;
using API;
using UnityEngine;
using BoomboxItem = LethalCompany::BoomboxItem;

[HarmonyPatch]
internal class BoomboxItemPatch
{
    private static readonly ManualLogSource Logger = new ("LethalAPI.Core");

    [HarmonyPatch(typeof(BoomboxItem), nameof(BoomboxItem.Start))]
    [HarmonyPostfix]
    internal static void Start(ref AudioClip?[] ___musicAudios)
    {
        Logger.LogInfo("Adding custom boombox audio clips...");

        var audioClips = new List<AudioClip?>(___musicAudios);
        audioClips.AddRange(Boombox.Clips);

        ___musicAudios = audioClips.ToArray();
    }
}

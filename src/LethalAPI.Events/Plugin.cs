// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="LethalLib">
// Copyright (c) LethalLib. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Events;

using BepInEx;
using HarmonyLib;
using HarmonyLib.Tools;
using static Handlers.Item.Boombox;
using static Handlers.Player;

/// <summary>
/// Events plugin for Lethal Company
/// </summary>
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private readonly Harmony _harmony = new (MyPluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION} is loading...");

        Logger.LogInfo("Installing harmony patches...");
        HarmonyFileLog.Enabled = true;
        _harmony.PatchAll();

        DontDestroyOnLoad(this);

        BoomboxStart += eventArgs =>
        {
            var clipName = eventArgs.Boombox.AudioSource.clip.name;
            if (clipName == "BoomboxMusic1")
            {
                Logger.LogInfo("BoomboxStart event triggered, but the boombox is playing BoomboxMusic1!");
                eventArgs.IsAllowed = false;
                return;
            }

            Logger.LogInfo("BoomboxStart event triggered!");
            Logger.LogInfo($"Playing: {eventArgs.Boombox.AudioSource.clip.name}");
        };

        PlayerEmoteStart += eventArgs =>
        {
            Logger.LogInfo("PlayerEmoteStart event triggered!");
            Logger.LogInfo($"Emote: {eventArgs.Player.Base.performingEmote}");
        };

        PlayerEmoteStop += eventArgs =>
        {
            Logger.LogInfo("PlayerEmoteStop event triggered!");
            Logger.LogInfo($"Emote: {eventArgs.Player.Base.performingEmote}");
        };

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}

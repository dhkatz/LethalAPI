// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="LethalAPI">
// Copyright (c) LethalAPI. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace LethalAPI.Core;

using API;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger { get; private set; } = null!;

    private Harmony Harmony { get; } = new (MyPluginInfo.PLUGIN_GUID);

    private void Awake()
    {
        Logger = base.Logger;

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION} is loading...");

        Logger.LogInfo("Installing harmony patches...");
        HarmonyFileLog.Enabled = true;
        Harmony.PatchAll();

        Logger.LogInfo("Loading boombox clips...");
        Boombox.Load();

        DontDestroyOnLoad(this);

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}

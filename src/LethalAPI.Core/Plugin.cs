namespace LethalAPI.Core;

using API;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using HarmonyLib.Tools;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private readonly Harmony _harmony = new (MyPluginInfo.PLUGIN_GUID);

    internal static new ManualLogSource Logger = null!;

    private void Awake()
    {
        Logger = base.Logger;

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION} is loading...");

        Logger.LogInfo("Installing harmony patches...");
        HarmonyFileLog.Enabled = true;
        _harmony.PatchAll();

        Logger.LogInfo("Loading boombox clips...");
        Boombox.Load();

        DontDestroyOnLoad(this);

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }
}

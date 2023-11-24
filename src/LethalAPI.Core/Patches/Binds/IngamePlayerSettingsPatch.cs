namespace LethalAPI.Core.Patches.Binds;

extern alias LethalCompany;
using HarmonyLib;
using JetBrains.Annotations;
using IngamePlayerSettings = LethalCompany::IngamePlayerSettings;

/// <summary>
/// Patches the <see cref="IngamePlayerSettings.ResetSettingsToDefault"/> method to reset the binds to the default binds.
/// </summary>
[HarmonyPatch(typeof(IngamePlayerSettings), nameof(IngamePlayerSettings.ResetSettingsToDefault))]
internal class IngamePlayerSettingsPatch
{
    [UsedImplicitly]
    internal static void Prefix(IngamePlayerSettings __instance)
    {

    }


    internal static void Postfix(IngamePlayerSettings __instance)
    {
        
    }
}

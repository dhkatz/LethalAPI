namespace LethalAPI.Core.Patches.Binds;

extern alias LethalCompany;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using LethalCompany::GameNetcodeStuff;
using UnityEngine.InputSystem;

[HarmonyPatch(typeof(PlayerControllerB))]
internal class PlayerControllerBPatch
{
    private static readonly List<PlayerControllerB> PlayerControllers = new ();

    public static void LoadBindings(string bindings)
    {
        foreach (var controller in PlayerControllers.Where(controller => controller is not null))
        {
            controller.playerActions.LoadBindingOverridesFromJson(bindings);
        }
    }

    [HarmonyPatch(nameof(PlayerControllerB.ConnectClientToPlayerObject))]
    [HarmonyPostfix]
    private static void PostfixConnectClientToPlayerObject(PlayerControllerB __instance)
    {
        PlayerControllers.Add(__instance);
        LoadBindings("");
    }
}

namespace LethalAPI.Core.Patches;

extern alias LethalCompany;
using HarmonyLib;
using Terminal = LethalCompany::Terminal;

[HarmonyPatch]
internal class TerminalPatch
{
    [HarmonyPatch(typeof(Terminal), nameof(Terminal.RunTerminalEvents))]
    [HarmonyPostfix]
    internal static void RunTerminalEvents(ref int ___groupCredits)
    {
        ___groupCredits = 50000;
    }
}

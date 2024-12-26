using System.Reflection;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Quests;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(Quest))]
[HarmonyPatch("GetMoneyReward")]
public static class IncreasedQuestMoneyRewardPatch1
{
    private static void Postfix(ref int __result)
    {
        __result = (int)((float)__result * 1.25f);
    }
}
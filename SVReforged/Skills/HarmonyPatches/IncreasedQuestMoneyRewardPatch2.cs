using System.Reflection;
using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Quests;
using StardewValley.SpecialOrders.Rewards;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(MoneyReward))]
[HarmonyPatch("GetRewardMoneyAmount")]
public static class IncreasedQuestMoneyRewardPatch2
{
    private static void Postfix(ref int __result)
    {
        __result = (int)((float)__result * 1.25f);
    }
}
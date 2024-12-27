using HarmonyLib;
using StardewValley.SpecialOrders.Rewards;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(MoneyReward))]
[HarmonyPatch("GetRewardMoneyAmount")]
public static class IncreasedQuestMoneyRewardPatch2
{
    private static void Postfix(ref int __result)
    {
        __result = (int)(__result * 1.25f);
    }
}
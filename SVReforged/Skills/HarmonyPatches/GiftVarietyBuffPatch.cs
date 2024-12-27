using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using Object = StardewValley.Object;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(NPC), nameof(NPC.receiveGift))]
public class GiftVarietyBuffPatch
{
    public static void Prefix(NPC __instance, Object o, ref float friendshipChangeMultiplier)
    {
        var name = __instance.Name;
        var chestID = "zmbchckn.SVReforged.NPCGiftHistoryGlobalChest_" + name;
        var giftHistoryInventory = Game1.player.team.GetOrCreateGlobalInventory(chestID);
        if (giftHistoryInventory == null) return;

        var alreadyGiftedCount = 0;

        foreach (var item in giftHistoryInventory)
        {
            ModEntry.SMonitor.Log($"item: {item.Name}", LogLevel.Info);
            if (item != null && item.ItemId == o.ItemId)
                alreadyGiftedCount++;
        }

        var buffPercent = 20 * (float)Math.Exp(-0.8 * alreadyGiftedCount);
        friendshipChangeMultiplier *= 1 + buffPercent / 100;

        giftHistoryInventory.Add(o);
    }
}
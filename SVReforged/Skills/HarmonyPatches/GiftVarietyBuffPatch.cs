using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Inventories;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(NPC), nameof(NPC.receiveGift))]
public class GiftVarietyBuffPatch
{
    public static void Prefix(NPC __instance,StardewValley.Object o, ref float friendshipChangeMultiplier)
    {
        string name = __instance.Name;
        string chestID = "zmbchckn.SVReforged.NPCGiftHistoryGlobalChest_" + name;
        Inventory giftHistoryInventory = Game1.player.team.GetOrCreateGlobalInventory(chestID);
        if (giftHistoryInventory == null) return;

        int alreadyGiftedCount = 0;

        foreach (Item item in giftHistoryInventory)
        {
            ModEntry.SMonitor.Log($"item: {item.Name}", LogLevel.Info);
            if (item != null && item.ItemId == o.ItemId)
                alreadyGiftedCount++;
        }
        float buffPercent = 20 * (float)Math.Exp(-0.8 * alreadyGiftedCount);
        friendshipChangeMultiplier *= (1 + buffPercent / 100);
        
        giftHistoryInventory.Add(o);
        
    }
}
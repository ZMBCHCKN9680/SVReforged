using HarmonyLib;
using StardewModdingAPI;
using StardewValley;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(NPC))]
[HarmonyPatch("receiveGift")]
public static class PartOfTheCommunityGiftReceivedPatch
{
    private static int initialFriendship;

    public static void Prefix(NPC __instance, Farmer giver)
    {
        if (giver.friendshipData.ContainsKey(__instance.Name)) initialFriendship = giver.friendshipData[__instance.Name].Points;
    }

    public static void Postfix(NPC __instance, Farmer giver)
    {
        if (giver.friendshipData.ContainsKey(__instance.Name))
        {
            var finalFriendship = giver.friendshipData[__instance.Name].Points;
            var friendshipChange = finalFriendship - initialFriendship;

            ModEntry.SMonitor.Log($"Friendship with {__instance.Name} changed by {friendshipChange} points after giving a gift.", LogLevel.Info);
            propagateFriendshipChange(__instance, friendshipChange);
        }
    }

    public static void propagateFriendshipChange(NPC __instance, int friendshipChange)
    {
        var propagateAmount = (int)(friendshipChange * 0.05);
        List<NPC> charactersInVicinity = getCharactersInVicinity(__instance.Tile, 40, Game1.currentLocation);
        foreach (var npc in charactersInVicinity)
        {
            ModEntry.SMonitor.Log($"{npc.Name}", LogLevel.Info);
            if (npc.Name == __instance.Name) continue;
            Game1.player.changeFriendship(propagateAmount, npc);
            ModEntry.SMonitor.Log($"Friendship with {npc.Name} changed by {propagateAmount} points after giving a gift.", LogLevel.Info);
        }
    }

    private static List<NPC> getCharactersInVicinity(Vector2 tile, float distance, GameLocation location)
    {
        List<NPC> charactersInVicinity = new();
        foreach (var character in location.characters)
            if (character != null && Vector2.Distance(character.Tile, tile) <= distance)
                charactersInVicinity.Add(character);

        return charactersInVicinity;
    }
}
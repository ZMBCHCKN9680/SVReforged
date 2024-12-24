using HarmonyLib;
using Newtonsoft.Json;
using StardewModdingAPI;
using StardewValley;

namespace SVReforged.Skills.HarmonyPatches;

[HarmonyPatch(typeof(NPC))]
[HarmonyPatch("receiveGift")]
public static class WordOfMouthGiftReceivedPatch
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
        var data = JsonConvert.DeserializeObject<CircleData>(File.ReadAllText(ModEntry.SHelper.DirectoryPath + "/assets/data/circleData.json"));
        foreach (var circle in data.Circles)
            if (circle.Centre.Equals(__instance.Name, StringComparison.OrdinalIgnoreCase))
            {
                foreach (var member in circle.Members)
                {
                    ModEntry.SMonitor.Log($"{member}", LogLevel.Info);
                    NPC npc;
                    try
                    {
                        npc = Game1.getCharacterFromName(member);
                    }
                    catch
                    {
                        continue;
                    }

                    if (npc != null)
                    {
                        Game1.player.changeFriendship((int)(friendshipChange * 0.05), npc);
                        ModEntry.SMonitor.Log($"Friendship with {npc.Name} changed by {(int)(friendshipChange * 0.05)} points after giving a gift.", LogLevel.Info);
                    }
                }

                break;
            }
    }
}

public class Circle
{
    public string Centre { get; set; }
    public List<string> Members { get; set; }
}

public class CircleData
{
    public List<Circle> Circles { get; set; }
}
using HarmonyLib;
using StardewValley;

namespace SVReforged.Skills.HarmonyPatches;

public class OutsiderGiftBuffPatch
{
    public static void ApplyPatch()
    {
        var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        harmony.Patch(
            AccessTools.Method(typeof(Farmer), nameof(Farmer.changeFriendship), new[] { typeof(int), typeof(NPC) }),
            new HarmonyMethod(typeof(OutsiderGiftBuffPatch), nameof(OutsiderGiftBuffPrefix))
        );
    }

    private static void OutsiderGiftBuffPrefix(ref int amount, NPC n)
    {
        List<string> ignoreNPC = new()
        {
            "Alex", "Elliott", "Harvey", "Sam", "Sebastian", "Shane",
            "Abigail", "Emily", "Haley", "Leah", "Maru", "Penny",
            "Caroline", "Clint", "Demetrius", "Evelyn", "George", "Gus", "Jas", "Jodi", "Kent", "Lewis", "Linus", "Marnie", "Pam", "Pierre", "Robin", "Sandy", "Vincent", "Willy"
        };
        if (ignoreNPC.Contains(n.Name))
            return;
        amount = (int)(amount * 1.1f);
    }
}
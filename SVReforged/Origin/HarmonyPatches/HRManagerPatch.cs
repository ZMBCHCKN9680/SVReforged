using HarmonyLib;
using StardewValley;

namespace SVReforged.HarmonyPatches;

public class HRManagerPatch
{
    public static void ApplyPatch()
    {
        var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        harmony.Patch(
            AccessTools.Method(typeof(Farmer), nameof(Farmer.changeFriendship), new[] { typeof(int), typeof(NPC) }),
            new HarmonyMethod(typeof(HRManagerPatch), nameof(HRManagerFriendshipBuffPrefix))
        );
    }

    private static void HRManagerFriendshipBuffPrefix(ref int amount, NPC n)
    {
        amount = (int)(amount * 1.1f);
    }
}
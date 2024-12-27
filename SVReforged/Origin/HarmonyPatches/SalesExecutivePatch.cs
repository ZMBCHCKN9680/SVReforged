using HarmonyLib;
using Object = StardewValley.Object;

namespace SVReforged.HarmonyPatches;

public class SalesExecutivePatch
{
    public static void ApplyPatch()
    {
        var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        harmony.Patch(
            AccessTools.Method(
                typeof(Object),
                nameof(Object.sellToStorePrice),
                new[] { typeof(long) }),
            postfix: new HarmonyMethod(typeof(SalesExecutivePatch), nameof(SalesExecutivePriceBuff))
        );
    }

    private static void SalesExecutivePriceBuff(ref int result)
    {
        result = (int)(result * 1.5f);
    }
}
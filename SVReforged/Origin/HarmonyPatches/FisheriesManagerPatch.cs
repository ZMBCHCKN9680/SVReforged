using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace SVReforged.HarmonyPatches;

public class FisheriesManagerPatch
{
    public PatchEnum.PatchName patchName => PatchEnum.PatchName.FISHERIES_MANAGER_PASSIVE;

    public static void ApplyPatch()
    {
        var constructor = AccessTools.Constructor(
            typeof(BobberBar),
            new[]
            {
                typeof(string),
                typeof(float),
                typeof(bool),
                typeof(List<string>),
                typeof(string),
                typeof(bool),
                typeof(string),
                typeof(bool)
            }
        );

        var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        if (constructor != null) harmony.Patch(constructor, postfix: new HarmonyMethod(typeof(FisheriesManagerPatch), nameof(BobberBarConstructorPostfix)));
    }

    private static void BobberBarConstructorPostfix(BobberBar __instance)
    {
        ModEntry.SMonitor.Log($"Bobber bar height: {__instance.bobberBarHeight}", LogLevel.Debug);
        __instance.bobberBarHeight = 192 + Game1.player.FishingLevel * 8;
        if (Game1.player.FishingLevel < 5 && __instance.beginnersRod) __instance.bobberBarHeight += 40 - Game1.player.FishingLevel * 8;

        __instance.bobberBarHeight += Utility.getStringCountInList(__instance.bobbers, "(O)695") * 24;
        if (__instance.setFlagOnCatch == "(O)DeluxeBait") __instance.bobberBarHeight += 12;
        __instance.bobberBarPos = 568 - __instance.bobberBarHeight;
    }
}
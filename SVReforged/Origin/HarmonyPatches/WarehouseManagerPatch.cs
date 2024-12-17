using HarmonyLib;
using StardewValley.Objects;

namespace SVReforged.HarmonyPatches;

public class WarehouseManagerPatch
{
    public PatchEnum.PatchName patchName => PatchEnum.PatchName.WAREHOUSE_MANAGER_PASSIVE;

    public static void ApplyPatch()
    {
        var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        harmony.Patch(
            AccessTools.Method(typeof(Chest), nameof(Chest.GetActualCapacity)),
            postfix: new HarmonyMethod(typeof(WarehouseManagerPatch), nameof(ChestSizeIncreasePostfix))
        );
    }

    public static void ChestSizeIncreasePostfix(Chest __instance, ref int __result)
    {
        switch (__instance.SpecialChestType)
        {
            case Chest.SpecialChestTypes.MiniShippingBin:
            case Chest.SpecialChestTypes.JunimoChest:
                __result = 18;
                break;
            case Chest.SpecialChestTypes.Enricher:
                __result = 1;
                break;
            case Chest.SpecialChestTypes.BigChest:
                __result = 80;
                break;
            default:
                __result = 45;
                break;
        }
    }
}
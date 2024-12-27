using HarmonyLib;
using StardewValley;
using StardewValley.GameData.Shops;
using StardewValley.Internal;
using StardewValley.Tools;

namespace SVReforged.HarmonyPatches;

public class AccountantPatch
{
    public static void ApplyPatch()
    {
        var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        harmony.Patch(
            AccessTools.Method(typeof(ShopBuilder), nameof(ShopBuilder.GetShopStock), new[] { typeof(string), typeof(ShopData) }),
            postfix: new HarmonyMethod(typeof(AccountantPatch), nameof(ChangeUpgradeCostPostfix))
        );
    }

    public static void ChangeUpgradeCostPostfix(string shopId, ref Dictionary<ISalable, ItemStockInformation> __result)
    {
        if (shopId != Game1.shop_blacksmithUpgrades)
            return;
        Dictionary<ISalable, ItemStockInformation> editedStock = new();
        foreach (var (item, stockInfo) in __result)
            if (item is Tool tool)
            {
                var upgradeLevel = tool.UpgradeLevel;
                editedStock[tool] = new ItemStockInformation(
                    GetUpgradeCost(upgradeLevel, item),
                    1,
                    GetUpgradeItemId(upgradeLevel),
                    GetUpgradeItemCount(upgradeLevel, item)
                );
            }
            else
            {
                editedStock[item] = stockInfo;
            }

        __result = editedStock;
    }

    private static int GetUpgradeCost(int upgradeLevel, ISalable item)
    {
        if (item is GenericTool tool)
        {
            if (upgradeLevel == 1)
                return (int)(1000 * 0.8f);
            if (upgradeLevel == 2)
                return (int)(2500 * 0.8f);
            if (upgradeLevel == 3)
                return (int)(5000 * 0.8f);
            if (upgradeLevel == 4)
                return (int)(12500 * 0.8f);

            throw new Exception("Invalid upgrade level");
        }

        if (upgradeLevel == 1)
            return (int)(2000 * 0.8f);
        if (upgradeLevel == 2)
            return (int)(5000 * 0.8f);
        if (upgradeLevel == 3)
            return (int)(10000 * 0.8f);
        if (upgradeLevel == 4)
            return (int)(25000 * 0.8f);

        throw new Exception("Invalid upgrade level");
    }

    private static int GetUpgradeItemCount(int upgradeLevel, ISalable item)
    {
        if (upgradeLevel == 1)
            return (int)(5 * 0.8f);
        if (upgradeLevel == 2)
            return (int)(5 * 0.8f);
        if (upgradeLevel == 3)
            return (int)(5 * 0.8f);
        if (upgradeLevel == 4)
            return (int)(5 * 0.8f);
        throw new Exception("Invalid upgrade level");
    }

    private static string GetUpgradeItemId(int upgradeLevel)
    {
        if (upgradeLevel == 1)
            return "334";
        if (upgradeLevel == 2)
            return "335";
        if (upgradeLevel == 3)
            return "336";
        if (upgradeLevel == 4)
            return "337";
        throw new Exception("Invalid upgrade level");
    }
}
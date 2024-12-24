using StardewModdingAPI.Events;
using StardewValley.GameData.Buildings;

namespace SVReforged.EventHandler;

public class SiteAcquisitionLeadEventHandler
{
    public static void InitiaizeEventListener()
    {
        ModEntry.SHelper.Events.Content.AssetRequested += OnAssetRequested;
        ModEntry.SHelper.GameContent.InvalidateCache("Data/Buildings");
    }

    public static void OnAssetRequested(object sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/Buildings"))
            e.Edit(asset =>
            {
                var data = asset.AsDictionary<string, BuildingData>().Data; // Correct type here
                foreach (var key in data.Keys)
                {
                    var buildingData = data[key];
                    buildingData.BuildCost = 13;

                    if (buildingData.BuildMaterials == null || buildingData.BuildMaterials.Count == 0)
                        continue;

                    foreach (var material in buildingData.BuildMaterials)
                        if (string.IsNullOrEmpty(material.ItemId))
                            continue;

                    for (var i = 0; i < buildingData.BuildMaterials.Count; i++)
                        buildingData.BuildMaterials[i] = new BuildingMaterial
                        {
                            ItemId = buildingData.BuildMaterials[i].ItemId,
                            Amount = 1
                        };
                }
            });
    }
}
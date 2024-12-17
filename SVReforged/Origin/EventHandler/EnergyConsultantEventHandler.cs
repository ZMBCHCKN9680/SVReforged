using StardewModdingAPI.Events;
using StardewValley.GameData.Machines;

namespace SVReforged.EventHandler;

public class EnergyConsultantEventHandler
{
    public EventHandlerEnum.EventHandlerName eventHandlerName => EventHandlerEnum.EventHandlerName.ENERGY_CONSULTANT_FURNACE_LOAD;

    public static void InitiaizeEventListener()
    {
        ModEntry.SHelper.Events.Content.AssetRequested += OnAssetRequested;
        ModEntry.SHelper.GameContent.InvalidateCache("Data/Machines");
    }

    public static void OnAssetRequested(object sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/Machines"))
            e.Edit(asset =>
            {
                var data = asset.AsDictionary<string, MachineData>().Data;
                var machineKey = "(BC)13";
                if (data.ContainsKey(machineKey))
                {
                    var machineData = data[machineKey];
                    foreach (var outputRule in machineData.OutputRules)
                        if (outputRule.Id == "Default_Quartz")
                            outputRule.MinutesUntilReady = (int)(outputRule.MinutesUntilReady * 0.75f);
                }
            });
    }
}
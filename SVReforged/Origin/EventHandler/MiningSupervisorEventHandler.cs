using StardewModdingAPI.Events;
using StardewValley.GameData.Machines;

namespace SVReforged.EventHandler;

public class MiningSupervisorEventHandler
{
    public EventHandlerEnum.EventHandlerName eventHandlerName => EventHandlerEnum.EventHandlerName.MINING_SUPERVISOR_FURNACE_CRYSTALARIUM_LOAD;

    public static void InitiaizeEventListener()
    {
        ModEntry.SHelper.Events.Content.AssetRequested += OnAssetRequested;
        ModEntry.SHelper.GameContent.InvalidateCache("Data/Machines");
    }

    public static void OnAssetRequested(object sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/Machines"))
        {
            e.Edit(asset =>
            {
                var data = asset.AsDictionary<string, MachineData>().Data;
                var machineKey = "(BC)21";
                if (data.ContainsKey(machineKey))
                {
                    var machineData = data[machineKey];

                    if (machineData.ReadyTimeModifiers != null)
                        foreach (var modifier in machineData.ReadyTimeModifiers)
                            if (modifier.Amount > 0)
                                modifier.Amount /= 2f;
                }
            });
            e.Edit(asset =>
            {
                var data = asset.AsDictionary<string, MachineData>().Data;
                var machineKey = "(BC)13";
                if (data.ContainsKey(machineKey))
                {
                    var machineData = data[machineKey];
                    foreach (var outputRule in machineData.OutputRules)
                        if (outputRule.MinutesUntilReady > 0)
                            outputRule.MinutesUntilReady /= 2;
                }
            });
        }
    }
}
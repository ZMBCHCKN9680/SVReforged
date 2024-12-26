using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace SVReforged.Skills.EventHandler;

public class FriendshipMailEventHandler
{
    private static readonly Dictionary<string, int> oldFriendshipData = new();

    public static void InitiaizeEventListener()
    {
        ModEntry.SHelper.Events.Content.AssetRequested += OnAssetRequested;
        ModEntry.SHelper.GameContent.InvalidateCache("Data/Mail");
    }

    public static void OnAssetRequested(object sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/Mail"))
        {
            e.Edit(asset =>
            {
                var data = asset.AsDictionary<string, string>().Data;
                data.Add("myMailName", "Dear @,^I eat rocks. %item id (O)136 1 (O)143 1 (O)202 1 (O)227 1 (O)228 1 %% %item id (O)136 1 (O)143 1 (O)202 1 (O)227 1 (O)228 1 %%[#]A Gift From Pam");
            });
        }
    }
}
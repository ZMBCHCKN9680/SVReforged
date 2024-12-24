using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace SVReforged.Skills.EventHandler;

public class StopFriendshipDecayEventHandler
{
    private static readonly Dictionary<string, int> oldFriendshipData = new();

    public static void InitiaizeEventListener()
    {
        ModEntry.SHelper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        ModEntry.SHelper.Events.GameLoop.UpdateTicked += OnUpdated;
        ModEntry.SHelper.Events.GameLoop.Saving += OnSaving;
    }

    public static void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
    {
        oldFriendshipData.Clear();
    }

    public static void OnUpdated(object sender, UpdateTickedEventArgs e)
    {
        if (e.IsOneSecond && Context.IsWorldReady)
            undoDecay();
    }

    public static void OnSaving(object sender, SavingEventArgs e)
    {
        undoDecay();
    }

    public static void storeFriendshipData()
    {
        oldFriendshipData.Clear();
        foreach ((var name, NetRef<Friendship> friendship) in Game1.player.friendshipData.FieldDict)
            oldFriendshipData[name] = friendship.Value.Points;
    }

    public static void undoDecay()
    {
        if (oldFriendshipData.Any())
            foreach (var name in Game1.player.friendshipData.Keys)
            {
                var friendship = Game1.player.friendshipData[name];
                if (oldFriendshipData.TryGetValue(name, out var oldValue) && oldValue > friendship.Points)
                    friendship.Points = oldValue;
            }

        storeFriendshipData();
    }
}
using SpaceCore;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using SVReforged.Skills.EventHandler;

namespace SVReforged.Skills;

public class Skills
{
    public static SpaceCore.Skills.Skill skill;

    public Skills()
    {
        SpaceCore.Skills.RegisterSkill(skill = new SocializingSkill.SocializingSkill());
        ModEntry.SHelper.Events.GameLoop.SaveLoaded += OnSaveLoaded;
        ModEntry.SHelper.Events.GameLoop.DayStarted += OnDayStarted;
    }

    public void OnSaveLoaded(object sender, SaveLoadedEventArgs e)
    {
        Game1.player.friendshipData["Robin"].Points = 800;
        StopFriendshipDecayEventHandler.InitiaizeEventListener();
    }

    public void OnDayStarted(object sender, DayStartedEventArgs e)
    {
        ModEntry.SMonitor.Log("Robin friendship : " + Game1.player.friendshipData["Robin"].Points, LogLevel.Info);
        //Game1.player.AddCustomSkillExperience(skill, 101);
        var level = Game1.player.GetCustomSkillLevel(skill);
        if (level >= 1) //stop friendship decay
        {
        }
        // if (level >= 1) //Birthday notification
        // {
        //     foreach (GameLocation? location in Game1.locations)
        //     {
        //         foreach (NPC? character in location.characters)
        //         {
        //             if (character.isBirthday())
        //             {
        //                 String name = character.Name;
        //                 string birthdayNotification;
        //                 if (name.EndsWith("s", StringComparison.OrdinalIgnoreCase))
        //                 {
        //                     birthdayNotification = "It's " + name + "' birthday today!";
        //                 }
        //                 else
        //                 {
        //                     birthdayNotification = "It's " + name + "'s birthday today!";
        //                 }
        //                 Game1.addHUDMessage(new HUDMessage(birthdayNotification,1));
        //             }
        //         }
        //     }
        // }
        // if (level >= 2) //Word of mouth
        // {
        //     var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        //     var targetMethod = AccessTools.Method(typeof(NPC), "receiveGift");
        //     
        //     var prefix = AccessTools.Method(typeof(WordOfMouthGiftReceivedPatch), "Prefix");
        //     var postfix = AccessTools.Method(typeof(WordOfMouthGiftReceivedPatch), "Postfix");
        //     
        //     harmony.Patch(targetMethod, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
        //     
        //     ModEntry.SMonitor.Log("SocialNetworkGiftReceivedPatch successfully applied.", LogLevel.Info);
        // }
        //
        // if (level >= 3) //Part of the community
        // {
        //     var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        //     var targetMethod = AccessTools.Method(typeof(NPC), "receiveGift");
        //     var prefix = AccessTools.Method(typeof(PartOfTheCommunityGiftReceivedPatch), "Prefix");
        //     var postfix = AccessTools.Method(typeof(PartOfTheCommunityGiftReceivedPatch), "Postfix");
        //     harmony.Patch(targetMethod, new HarmonyMethod(prefix), new HarmonyMethod(postfix));
        // }
    }
}
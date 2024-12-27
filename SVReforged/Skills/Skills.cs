using SpaceCore;
using StardewModdingAPI.Events;
using StardewValley;

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
        //Game1.player.friendshipData["Robin"].Points = 800;
        //StopFriendshipDecayEventHandler.InitiaizeEventListener();
    }

    public void OnDayStarted(object sender, DayStartedEventArgs e)
    {
        Game1.player.AddCustomSkillExperience(skill, 101);

        var level = Game1.player.GetCustomSkillLevel(skill);

        if (level >= 1)
        {
            // FriendshipMailEventHandler.InitiaizeEventListener();
            // Game1.addMailForTomorrow("myMailName");
        }
        // if (level >= 1)
        // {
        //     FriendshipMailEventHandler.InitiaizeEventListener();
        //     Game1.addMailForTomorrow("myMailName");
        // }
        // if (level >= 1)
        // {
        //     var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        //     var targetMethod = AccessTools.Method(typeof(NPC), "receiveGift");
        //     var prefix = AccessTools.Method(typeof(GiftVarietyBuffPatch), "Prefix");
        //     harmony.Patch(targetMethod, new HarmonyMethod(prefix), null);
        // }
        // if (level >= 1) //increase quest reward
        // {
        //     var harmony = new Harmony(ModEntry.SModManifest.UniqueID);
        //     var targetMethod = AccessTools.Method(typeof(Quest), "GetMoneyReward");
        //     var postfix = AccessTools.Method(typeof(IncreasedQuestMoneyRewardPatch1), "Postfix");
        //     harmony.Patch(targetMethod, null, new HarmonyMethod(postfix));
        //     
        //     var targetMethod2 = AccessTools.Method(typeof(MoneyReward), "GetRewardMoneyAmount");
        //     var postfix2 = AccessTools.Method(typeof(IncreasedQuestMoneyRewardPatch2), "Postfix");
        //     harmony.Patch(targetMethod2, null, new HarmonyMethod(postfix2));
        // }
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
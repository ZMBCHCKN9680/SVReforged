using StardewModdingAPI;
using StardewValley;

namespace SVReforged.Skills.SocializingSkill;

public class SocializingSkill : SpaceCore.Skills.Skill
{
    public static readonly string SocializingSkillId = "zmbchckn.SVReforged.SocializingSkill";
    public static readonly string SocializingSkillName = "Socializing";
    public static IList<Profession> Professions = new List<Profession>();

    public SocializingSkill() : base(SocializingSkillId)
    {
        Icon = ModEntry.SHelper.ModContent.Load<Texture2D>("C:\\Users\\admin\\RiderProjects\\SVReforged\\SVReforged\\assets\\sprites\\iconA.png");
        SkillsPageIcon = ModEntry.SHelper.ModContent.Load<Texture2D>("C:\\Users\\admin\\RiderProjects\\SVReforged\\SVReforged\\assets\\sprites\\iconB.png");
        ExperienceCurve = new[] { 100, 200, 300, 1300, 2150, 3300, 4800, 6900 };
        ExperienceBarColor = new Color(196, 76, 255);
    }

    public override string GetName()
    {
        return SocializingSkillName;
    }

    public override void DoLevelPerk(int level)
    {
        if (level == 2)
            foreach (var location in Game1.locations)
            foreach (var character in location.characters)
            {
                var name = character.Name;
                var chestID = "zmbchckn.SVReforged.NPCGiftHistoryGlobalChest_" + name;
                Game1.player.team.GetOrCreateGlobalInventory(chestID);
                ModEntry.SMonitor.Log($"Created chest for {name}", LogLevel.Info);
            }
    }
}
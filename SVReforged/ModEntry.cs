using StardewModdingAPI;
using StardewUI.Framework;

namespace SVReforged;

public class ModEntry : Mod
{
    public static IMonitor? SMonitor;
    public static IManifest? SModManifest;
    public static IModHelper? SHelper;
    private IViewEngine viewEngine;

    public override void Entry(IModHelper helper)
    {
        SMonitor = Monitor;
        SHelper = helper;
        SModManifest = ModManifest;
        var introDriver = new Intro.Intro();
        //var originDriver = new Origin.Origin();
        var skillDriver = new Skills.Skills();
    }
}
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewUI.Framework;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using StardewValley.Objects;
using SVReforged.EventHandler;
using SVReforged.HarmonyPatches;
using Object = StardewValley.Object;

namespace SVReforged.Intro;

public class Intro
{
    public Intro()
    {
        ModEntry.SHelper.Events.Display.RenderedHud += OnRenderedHud;
    }
    
    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
        CodexButton codexButton = new CodexButton(e.SpriteBatch);
        Game1.onScreenMenus.Add(codexButton);
    }
}
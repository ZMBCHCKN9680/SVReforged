using StardewModdingAPI.Events;
using StardewValley;

namespace SVReforged.Intro;

public class Intro
{
    public Intro()
    {
        ModEntry.SHelper.Events.Display.RenderedHud += OnRenderedHud;
    }

    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
        var codexButton = new CodexButton(e.SpriteBatch);
        Game1.onScreenMenus.Add(codexButton);
    }
}
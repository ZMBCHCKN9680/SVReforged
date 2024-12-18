using System.ComponentModel;
using PropertyChanged.SourceGenerator;
using StardewModdingAPI;
using StardewUI.Framework;
using StardewValley;
using StardewValley.Menus;

namespace SVReforged.Intro;

public class CodexMenu : IClickableMenu
{

    private IViewEngine viewEngine;
    private string viewAssetPrefix = "Mods/SVReforged/Views";
    public CodexMenu()
    {
        viewEngine = ModEntry.SHelper.ModRegistry.GetApi<IViewEngine>("focustense.StardewUI");
        viewEngine.RegisterViews(viewAssetPrefix, "assets/views");
    }

    public void RenderCodexMenu()
    {
        var context = new TabModel()
        {
            Tabs = new List<TabData>
            {
                new(0,"Home", Game1.mouseCursors, new Rectangle(20, 388, 8, 8)) { ifTabActive = true },
                new(1,"Social", Game1.mouseCursors, new Rectangle(36, 374, 7, 8)),
                new(2,"Money", Game1.mouseCursors, new Rectangle(4, 388, 8, 8)),
                new(3,"Seasons", Game1.mouseCursors, new Rectangle(420, 1204, 8, 8))
            }
        };
        Game1.activeClickableMenu = viewEngine.CreateMenuFromAsset($"{viewAssetPrefix}/CodexMenuView",context);
    }
}

public partial class TabModel
{
    public IReadOnlyList<TabData> Tabs { get; set; }
    
    public void OnTabActivatedSetOtherTabsInactive(int index)
    {
        ModEntry.SMonitor.Log("OnTabActivatedSetOtherTabsInactive with index: " + index, LogLevel.Debug);
        foreach (var tab in Tabs)
        {
            if (tab.tabIndex != index)
                tab.IfTabActive = false;
        }
    }
}

public partial class TabData
{
    public int tabIndex { get; }
    public string tabName { get; }
    public Tuple<Texture2D, Rectangle> Sprite { get; }
    [Notify] public bool ifTabActive;
    
    public TabData(int index, string name, Texture2D texture, Rectangle sourceRect)
    {
        tabIndex = index;
        tabName = name;
        Sprite = Tuple.Create(texture, sourceRect);
    }
}
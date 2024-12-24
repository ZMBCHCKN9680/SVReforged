using PropertyChanged.SourceGenerator;
using StardewUI.Framework;
using StardewValley;
using StardewValley.Menus;

namespace SVReforged.Intro;

public class CodexMenu : IClickableMenu
{
    private readonly string viewAssetPrefix = "Mods/SVReforged/Views";
    private readonly IViewEngine viewEngine;

    public CodexMenu()
    {
        viewEngine = ModEntry.SHelper.ModRegistry.GetApi<IViewEngine>("focustense.StardewUI");
        viewEngine.RegisterViews(viewAssetPrefix, "assets/views");
    }

    public void RenderCodexMenu()
    {
        var context = new TabModel
        {
            Tabs = new List<TabData>
            {
                new(0, "Home", Game1.mouseCursors, new Rectangle(20, 388, 8, 8)) { ifTabActive = true },
                new(1, "Skills", Game1.mouseCursors, new Rectangle(369, 16, 14, 16)),
                new(2, "Journal", Game1.mouseCursors, new Rectangle(229, 410, 14, 14))
            }
        };
        Game1.activeClickableMenu = viewEngine.CreateMenuFromAsset($"{viewAssetPrefix}/CodexMenuView", context);
    }
}

public class TabModel
{
    public IReadOnlyList<TabData> Tabs { get; set; }

    public void OnTabActivatedSetOtherTabsInactive(int index)
    {
        foreach (var tab in Tabs)
            if (tab.tabIndex != index)
                tab.IfTabActive = false;
    }
}

public partial class TabData
{
    [Notify] public bool ifTabActive;

    public TabData(int index, string name, Texture2D texture, Rectangle sourceRect)
    {
        tabIndex = index;
        tabName = name;
        Sprite = Tuple.Create(texture, sourceRect);
    }

    public int tabIndex { get; }
    public string tabName { get; }
    public Tuple<Texture2D, Rectangle> Sprite { get; }
}
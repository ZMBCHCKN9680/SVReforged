using System.ComponentModel;
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
        var context = new TabsViewModel()
        {
            Tabs = new List<TabData>
            {
                new("Home", Game1.mouseCursors, new Rectangle(20, 388, 8, 8)) { Active = true },
                new("Social", Game1.mouseCursors, new Rectangle(36, 374, 7, 8)),
                new("Money", Game1.mouseCursors, new Rectangle(4, 388, 8, 8)),
                new("Seasons", Game1.mouseCursors, new Rectangle(420, 1204, 8, 8))
            }
        };
        Game1.activeClickableMenu = viewEngine.CreateMenuFromAsset($"{viewAssetPrefix}/CodexMenuView",context);
    }
}

partial class TabData : INotifyPropertyChanged
{
    public string Name { get; }
    public Tuple<Texture2D, Rectangle> Sprite { get; }

    private bool _active;

    public bool Active
    {
        get => _active;
        set
        {
            if (_active != value)
            {
                _active = value;
                OnPropertyChanged(nameof(Active));
            }
        }
    }
    
    public TabData(string name, Texture2D texture, Rectangle sourceRect)
    {
        Name = name;
        Sprite = Tuple.Create(texture, sourceRect);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

partial class TabsViewModel
{
    public IReadOnlyList<TabData> Tabs { get; set; } = new List<TabData>();

    public void OnTabActivated(string name)
    {
        foreach (var tab in Tabs)
        {
            if (tab.Name != name)
            {
                tab.Active = false;
            }
        }
    }
}


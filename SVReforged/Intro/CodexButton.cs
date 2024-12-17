using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;

namespace SVReforged.Intro;

public class CodexButton : IClickableMenu
{
    public string hoverText = "SVR Codex";
    private static float spriteScale = 4f;
    private static int width = 11;
    private static int height = 14;
    private static int xAnchor = 10;
    private static int yAnchor = 10;
    private ClickableTextureComponent codexIcon;
    private CodexMenu codexMenu;

    public CodexButton(SpriteBatch batch)
    {
        codexIcon = new ClickableTextureComponent(new Rectangle(xAnchor, yAnchor, width*4, height*4), Game1.mouseCursors, new Rectangle(383, 493, 11, 14), spriteScale);
        //codexIcon = new ClickableTextureComponent("codexIcon",new Rectangle(xAnchor, yAnchor, width*4, height*4),null,hoverText,Game1.mouseCursors,new Rectangle(383, 493, 11, 14),spriteScale);
        this.initialize(10,10,11*4,14*4);
        setupOnHover(batch);
        codexMenu = new CodexMenu();
    }

    public override void draw(SpriteBatch b)
    {
        codexIcon.draw(b);
    }

    public void setupOnHover(SpriteBatch b)
    {
        if (codexIcon.containsPoint(Game1.getMousePosition().X, Game1.getMousePosition().Y))
        {
            IClickableMenu.drawHoverText(b, hoverText, Game1.smallFont);
        }
    }

    public override void receiveLeftClick(int x, int y,bool playSound = true)
    {
        if (this.codexIcon.containsPoint(x, y))
        {
            codexMenu.RenderCodexMenu();
        }
    }
}
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace StoneforgeGame.Game.Libraries;


public static class FontLibrary {
    // FIELDS
    private static ContentManager _content;

    public static SpriteFont TempFont;


    // CONSTRUCTORS



    // PROPERTIES
    public static ContentManager Content {
        set => _content = value;
    }


    // METHODS
    public static void Load() {
        TempFont = _content.Load<SpriteFont>("Assets/Fonts/TempFont");
    }
}

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Libraries;


public static class TextureLibrary {
    // FIELDS
    private static ContentManager _content;
    
    public static Texture TempBackground;
    public static Texture TempCharacter64;
    public static Texture Batumbakal;
    


    // CONSTRUCTORS
    


    // PROPERTIES
    public static ContentManager Content {
        set => _content = value;
    }


    // METHODS
    public static void Load() {
        TempBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/tempBackground"),
            1, 1
        );
        TempCharacter64 = new Texture(
            _content.Load<Texture2D>("Assets/Textures/tempCharacter64x64"),
            1, 1
        );
        
        Batumbakal = new Texture(
            _content.Load<Texture2D>("Assets/Textures/Batumbakal"),
            5, 12
        );
    }
}

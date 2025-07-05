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
    public static Texture StageOneBackground;
    public static Texture StageTwoBackground;
    public static Texture StageThreeBackground;
    


    // CONSTRUCTORS
    


    // PROPERTIES
    public static ContentManager Content {
        set => _content = value;
    }


    // METHODS
    public static void Load() {
        #region Temporary Textures
        TempBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/tempBackground"),
            1, 1
        );
        TempCharacter64 = new Texture(
            _content.Load<Texture2D>("Assets/Textures/tempCharacter64x64"),
            1, 1
        );
        #endregion
        
        Batumbakal = new Texture(
            _content.Load<Texture2D>("Assets/Textures/Batumbakal"),
            5, 12
        );
        
        StageOneBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/StageOneBackground"),
            1, 1
        );
        
        StageTwoBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/StageTwoBackground"),
            1, 1
        );

        StageThreeBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/StageThreeBackground"),
            1, 1
        );
    }
}

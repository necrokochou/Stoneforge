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
    public static Texture RockPile;
    public static Texture RedGem;
    public static Texture BlueGem;
    public static Texture GreenGem;
    public static Texture Altar;
    public static Texture SkeletonIdle;
    public static Texture SkeletonWalk;
    public static Texture SkeletonAttack;
    public static Texture NightBorne;
    public static Texture GolluxIdle;
    public static Texture GolluxMove;
    public static Texture GolluxAttack;
    public static Texture MenuBackground;


    // CONSTRUCTORS
    


    // PROPERTIES
    public static ContentManager Content {
        set => _content = value;
    }


    // METHODS
    public static void Load() {
        #region --- Temporary Texture ---
        TempBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/tempBackground"),
            1,
            1
        );
        TempCharacter64 = new Texture(
            _content.Load<Texture2D>("Assets/Textures/tempCharacter64x64"),
            1,
            1
        );
        #endregion
        
        Batumbakal = new Texture(
            _content.Load<Texture2D>("Assets/Textures/Batumbakal"),
            6,
            12
        );
        
        StageOneBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/StageOneBackground"),
            1,
            1
        );
        
        StageTwoBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/StageTwoBackground"),
            1,
            1
        );

        StageThreeBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/StageThreeBackground"),
            1,
            1
        );

        RockPile = new Texture(
            _content.Load<Texture2D>("Assets/Textures/RockPile"),
            1,
            1
        );

        RedGem = new Texture(
            _content.Load<Texture2D>("Assets/Textures/RedGem"),
            1,
            10
        );

        BlueGem = new Texture(
            _content.Load<Texture2D>("Assets/Textures/BlueGem"),
            1,
            11
        );

        GreenGem = new Texture(
            _content.Load<Texture2D>("Assets/Textures/GreenGem"),
            1,
            10
        );

        Altar = new Texture(
            _content.Load<Texture2D>("Assets/Textures/Altar"),
            1,
            11
        );

        SkeletonIdle = new Texture(
            _content.Load<Texture2D>("Assets/Textures/SkeletonIdle"),
            1,
            4
        );

        SkeletonWalk = new Texture(
            _content.Load<Texture2D>("Assets/Textures/SkeletonWalk"),
            1,
            4
        );

        SkeletonAttack = new Texture(
            _content.Load<Texture2D>("Assets/Textures/SkeletonAttack"),
            1,
            8
        );

        NightBorne = new Texture(
            _content.Load<Texture2D>("Assets/Textures/NightBorne"),
            5,
            23
        );

        MenuBackground = new Texture(
            _content.Load<Texture2D>("Assets/Textures/MenuBackground"),
            1,
            1
        );
    }
}

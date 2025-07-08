using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneforgeGame.Game.Entities.ObjectTiles;


namespace StoneForgeGame.Game.Utilities;


public static class MyDebug {
    // FIELDS
    private static GraphicsDevice _graphicsDevice;
    private static Texture2D _texture;
    private static Rectangle _window;
    
    public static bool IsDebug = false;
    

    // CONSTRUCTORS
    


    // PROPERTIES
    public static GraphicsDevice Graphics {
        set => _graphicsDevice = value;
    }
    public static Texture2D Texture {
        get {
            _texture = new Texture2D(_graphicsDevice, 1, 1);
            _texture.SetData([Color.White]);
            return _texture;
        }
    }
    public static Rectangle Window {
        get => _window;
        set => _window = value;
    }


    // METHODS
    public static void DrawRect(SpriteBatch spriteBatch, Rectangle rect, Color color) {
        spriteBatch.Draw(Texture, rect, color);
    }
    
    public static void DrawHollowRect(SpriteBatch spriteBatch, Rectangle rect, Color color, int thickness = 1) {
        // TOP
        spriteBatch.Draw(Texture, new Rectangle(rect.X, rect.Y, rect.Width, thickness), color);
        // BOTTOM
        spriteBatch.Draw(Texture, new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness), color);
        // LEFT
        spriteBatch.Draw(Texture, new Rectangle(rect.X, rect.Y, thickness, rect.Height), color);
        // RIGHT
        spriteBatch.Draw(Texture, new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), color);
    }
}

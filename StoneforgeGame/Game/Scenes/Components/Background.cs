using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Scenes.Components;


public class Background {
    // FIELDS
    private Texture _texture;
    private Rectangle _destination;


    // CONSTRUCTORS
    public Background(Texture texture, Point size) {
        _texture = texture;
        _destination = new Rectangle(Point.Zero, size);
    }


    // PROPERTIES



    // METHODS
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(_texture.Image, _destination, Color.White);
    }
}

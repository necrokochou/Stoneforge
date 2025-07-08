using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;


namespace StoneforgeGame.Game.Entities.ObjectTiles;


public class RockPile : ObjectTile {
    // FIELDS



    // CONSTRUCTORS
    public RockPile(Stage stage) {
        Texture = TextureLibrary.RockPile;

        Size = new Point(108, 68);

        Stage = stage;

        IsDestroyable = true;
        IsInteractable = false;
    }


    // PROPERTIES



    // METHODS
    public override void Load(Point location) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;

        Source = new Rectangle(
            frameWidth * 0,
            frameHeight * 0,
            frameWidth,
            frameHeight
        );

        Destination = new Rectangle(
            location,
            Size
        );

        Color = Color.White;

        CollisionBox = new BoxCollider(
            location,
            location + Size,
            solid : false
        );
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture.Image, Destination, Source, Color);
    }

    protected override void OnDestroy() { }
}

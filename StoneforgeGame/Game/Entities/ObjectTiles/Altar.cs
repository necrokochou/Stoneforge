using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;


namespace StoneforgeGame.Game.Entities.ObjectTiles;


public class Altar : ObjectTile {
    // FIELDS
    
    

    // CONSTRUCTORS
    public Altar(Stage stage) {
        Texture = TextureLibrary.Altar;

        Size = new Point(200, 280);

        Stage = stage;
        
        IsDestroyable = false;
    }


    // PROPERTIES



    // METHODS
    public override void Load(Point location) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        Source = new Rectangle(
            frameWidth * 0, frameHeight * 0,
            frameWidth, frameHeight
        );
        
        Destination = new Rectangle(
            location, Size
        );
        
        Color = Color.White;

        CollisionBox = new BoxCollider(
            location,
            location + Size,
            solid : false
        );
        
        // ActualPosition = location.ToVector2();
        //
        // AnimationManager = null;
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture.Image, Destination, Source, Color);
    }

    public override void OnDestroy() {
        if (IsDestroyed) return;
        Destroy();
    }
}

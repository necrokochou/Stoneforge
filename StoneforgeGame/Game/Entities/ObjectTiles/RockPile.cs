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

        Stage = stage;
        
        IsDestroyable = true;
    }


    // PROPERTIES



    // METHODS
    public override void Load(Point location, Point size = default) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        if (size == default) size = new Point(frameWidth, frameHeight);
        
        Source = new Rectangle(
            frameWidth * 0, frameHeight * 0,
            frameWidth, frameHeight
        );
        Destination = new Rectangle(
            location.X, location.Y,
            size.X, size.Y
        );
        Color = Color.White;

        CollisionBox = new BoxCollider(
            Destination.Location,
            Destination.Location + Destination.Size,
            
            solid : false
        );
        
        // ActualPosition = location.ToVector2();
        //
        // AnimationManager = null;
    }

    public override void Update() {
        
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture.Image, Destination, Source, Color);
    }

    public override void OnDestroy() {
        if (IsDestroyed) return;
        Destroy();
    }
}

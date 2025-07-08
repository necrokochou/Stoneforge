using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Entities.ObjectTiles;


public class Altar : ObjectTile {
    // FIELDS
    
    

    // CONSTRUCTORS
    public Altar(Stage stage) {
        Texture = TextureLibrary.Altar;

        Size = new Point(200, 280);

        Stage = stage;
        
        IsDestroyable = false;
        IsInteractable = true;
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
    }
    
    public override void Update() { }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture.Image, Destination, Source, Color);
    }

    protected override void OnInteract(Character character) {
        if (character.GetGemCount() >= 3) {
            Color = Color.Black;
            IsCompleted = true;
            SaveManager.DeleteSave();
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;


namespace StoneforgeGame.Game.Scenes.Stages;


public abstract class Stage : Scene {
    // FIELDS
    protected CollisionManager CollisionManager = new CollisionManager();
    protected ObjectTileManager ObjectTileManager = new ObjectTileManager();
    protected CharacterManager CharacterManager = new CharacterManager();

    protected string Name;
    protected Character Player;

    protected Gravity Gravity;

    protected Rectangle NextSceneBounds;
    protected bool ReachedNextLocation;


    // CONSTRUCTORS
    


    // PROPERTIES
    public CollisionManager GetCollisionManager {
        get => CollisionManager;
    }
    public ObjectTileManager GetObjectTileManager {
        get => ObjectTileManager;
    }
    public CharacterManager GetCharacterManager {
        get => CharacterManager;
    }
    public bool ReachedNextSceneBounds {
        get => ReachedNextLocation;
    }


    // METHODS
    public override void Unload() {
        CharacterManager.Unload();
        CollisionManager.Unload();
        ObjectTileManager.Unload();
    }

    public override void Update(GameTime gameTime) {
        CharacterManager.Update(gameTime, Gravity);
        CollisionManager.Update();
        ObjectTileManager.Update();
        
        ReachedNextLocation = NextSceneBounds.Contains(Player.Collider.Bounds);
    }
    
    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        CollisionManager.Draw(spriteBatch);
        ObjectTileManager.Draw(spriteBatch);
        CharacterManager.Draw(spriteBatch);
    }
}

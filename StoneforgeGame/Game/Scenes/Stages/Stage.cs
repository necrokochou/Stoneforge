using System.Collections.Generic;
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
    protected List<Enemy> Enemies;

    protected Gravity Gravity;

    protected Rectangle NextSceneBounds;
    protected bool ReachedNextLocation;
    
    private bool _debug = true;


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
        
        ReachedNextLocation = NextSceneBounds.Contains(Player.GetCollisionBox().Bounds);

        if (ReachedNextLocation) {
            Player.ActualPosition = Vector2.Zero;
        }
    }
    
    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        if (_debug) CollisionManager.Draw(spriteBatch);
        ObjectTileManager.Draw(spriteBatch);
        CharacterManager.Draw(spriteBatch);
    }
    
    public string GetName() {
        return Name;
    }

    public Rectangle GetNextSceneBounds() {
        return NextSceneBounds;
    }
    
    public bool GetReachedNextLocation() {
        return ReachedNextLocation;
    }
}

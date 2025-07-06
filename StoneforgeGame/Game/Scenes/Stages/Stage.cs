using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Entities.Items.Collectable;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;


namespace StoneforgeGame.Game.Scenes.Stages;


public abstract class Stage : Scene {
    // FIELDS
    protected CollisionManager CollisionManager = new CollisionManager();
    protected ObjectTileManager ObjectTileManager = new ObjectTileManager();
    protected CharacterManager CharacterManager = new CharacterManager();
    protected ItemManager ItemManager = new ItemManager();

    protected string Name;
    protected Character Player;

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
    public ItemManager GetItemManager {
        get => ItemManager;
    }
    public bool ReachedNextSceneBounds {
        get => ReachedNextLocation;
    }


    // METHODS
    public override void Unload() {
        CharacterManager.Unload();
        CollisionManager.Unload();
        ObjectTileManager.Unload();
        ItemManager.Unload();
    }

    public override void Update(GameTime gameTime) {
        CharacterManager.Update(gameTime, Gravity);
        CollisionManager.Update();
        ObjectTileManager.Update();
        ItemManager.Update(gameTime);
        
        ReachedNextLocation = NextSceneBounds.Contains(Player.GetCollisionBox().Bounds);
    }
    
    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        if (_debug) CollisionManager.Draw(spriteBatch);
        ObjectTileManager.Draw(spriteBatch);
        CharacterManager.Draw(spriteBatch);
        ItemManager.Draw(spriteBatch);
    }
    
    public string GetName() {
        return Name;
    }
}

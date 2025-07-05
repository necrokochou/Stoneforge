using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;


namespace StoneforgeGame.Game.Scenes.Stages;


public abstract class Stage : Scene {
    // FIELDS
    protected CollisionManager CollisionManager;
    protected CharacterManager CharacterManager;

    protected string Name;
    protected Character Player;

    protected Gravity Gravity;

    protected Rectangle NextSceneBounds;
    protected bool ReachedNextLocation;


    // CONSTRUCTORS
    


    // PROPERTIES
    public bool ReachedNextSceneBounds {
        get => ReachedNextLocation;
    }


    // METHODS
    public override void Unload() {
        CollisionManager.Unload();
        CharacterManager.Unload();
    }

    public override void Update(GameTime gameTime) {
        CharacterManager.Update(gameTime, Gravity);
        
        ReachedNextLocation = NextSceneBounds.Contains(Player.Collider.Bounds);
    }
    
    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        CollisionManager.Draw(spriteBatch);
        CharacterManager.Draw(spriteBatch);
    }
}

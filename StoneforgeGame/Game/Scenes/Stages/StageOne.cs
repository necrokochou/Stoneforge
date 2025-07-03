using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Components;


namespace StoneforgeGame.Game.Scenes.Stages;


public class StageOne : Scene {
    // FIELDS
    private CollisionManager _collisionManager = new CollisionManager();
    private CharacterManager _characterManager;
    
    private string _name = "Heart of the Mountain";
    private Batumbakal _batumbakal;

    private Gravity _gravity;


    // CONSTRUCTORS
    


    // PROPERTIES



    // METHODS
    public override void Load() {
        Background = new Background(TextureLibrary.StageOneBackground, Window.Size);
        
        _collisionManager.SetBorder(32);
        _collisionManager.Add(new Point(0, 0), new Point(1920, 100));
        _collisionManager.Add(new Point(0, 900), new Point(1920, 1080));
        
        _batumbakal = new Batumbakal();
        _batumbakal.Load(new Point(100, 300));
        
        _collisionManager.Add(_batumbakal.Collider);
        
        _characterManager = new CharacterManager(_collisionManager);
        
        _characterManager.Add(_batumbakal);
        
        _gravity = new Gravity(980f, new Vector2(0, 1));
    }
    
    public override void Unload() {
        _characterManager.Unload();
    }

    public override void Update(GameTime gameTime) {
        _characterManager.Update(gameTime, _gravity);
    }
    
    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        _collisionManager.Draw(spriteBatch);
        _characterManager.Draw(spriteBatch);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Scenes;


namespace StoneForgeGame.Game.Managers;


public class SceneManager {
    // FIELDS
    private Scene _currentScene;


    // CONSTRUCTORS
    


    // PROPERTIES



    // METHODS'
    public void Load() {
        _currentScene?.Load();
    }

    public void Unload() {
        _currentScene?.Unload();
    }
    
    public void Update(GameTime gameTime) {
        _currentScene?.Update(gameTime);
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        _currentScene?.Draw(spriteBatch);
    }

    public void ChangeScene(Scene newScene) {
        Unload();
        _currentScene = newScene;
        Load();
    }
}

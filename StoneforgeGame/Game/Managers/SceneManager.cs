using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Scenes;
using StoneforgeGame.Game.Scenes.Stages;


namespace StoneForgeGame.Game.Managers;


public class SceneManager {
    // FIELDS
    private List<Scene> _scenes;
    private int _currentSceneIndex;
    private Scene _currentScene;
    private int _firstStageIndex;
    private int _lastStageIndex;


    // CONSTRUCTORS
    public SceneManager(Scene[] scenes) {
        _scenes = new List<Scene>(scenes);
        _currentSceneIndex = 0;
        _currentScene = _scenes[_currentSceneIndex];

        int stageCount = 0;
        foreach (Scene scene in _scenes) {
            if (scene is Stage) {
                stageCount++;
                if (stageCount == 1) {
                    _firstStageIndex = _scenes.IndexOf(scene);
                }
            }
        }
        _lastStageIndex = _scenes.IndexOf(scenes[_firstStageIndex + stageCount - 1]);
    }


    // PROPERTIES
    


    // METHODS'
    public void Load() {
        _currentScene.Load();
    }

    public void Unload() {
        _currentScene.Unload();
    }
    
    public void Update(GameTime gameTime) {
        _currentScene.Update(gameTime);
        
        if (_currentScene is Stage currentStage) {
            if (currentStage.ReachedNextSceneBounds && _currentSceneIndex < _lastStageIndex) {
                _currentScene.Unload();
                _currentSceneIndex++;
                _currentScene = _scenes[_currentSceneIndex];
                _currentScene.Load();
            }
        }
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        _currentScene.Draw(spriteBatch);
    }
}

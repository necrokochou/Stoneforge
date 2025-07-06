using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Scenes;
using StoneforgeGame.Game.Scenes.Stages;
using StoneforgeGame.Game.Utilities;


namespace StoneForgeGame.Game.Managers;


public class SceneManager {
    // FIELDS
    private List<Scene> _scenes;
    private int _currentSceneIndex;
    private Scene _currentScene;
    private int _firstStageIndex;
    private int _lastStageIndex;
    
    private Character _player;


    // CONSTRUCTORS
    // public SceneManager(Scene[] scenes) {
    //     _scenes = new List<Scene>(scenes);
    //     _currentSceneIndex = 0;
    //     _currentScene = _scenes[_currentSceneIndex];
    //
    //     int stageCount = 0;
    //     foreach (Scene scene in _scenes) {
    //         if (scene is Stage) {
    //             stageCount++;
    //             if (stageCount == 1) {
    //                 _firstStageIndex = _scenes.IndexOf(scene);
    //             }
    //         }
    //     }
    //     _lastStageIndex = _scenes.IndexOf(scenes[_firstStageIndex + stageCount - 1]);
    // }
    public SceneManager() {
        _scenes = new List<Scene>();
    }


    // PROPERTIES
    


    // METHODS'
    public void Load() {
        SaveData saveData = SaveManager.Load();
        _player = new Batumbakal();

        if (saveData != null) {
            _player.ActualPosition = new Vector2(saveData.PositionX, saveData.PositionY);
            _player.GetHealth().Current = saveData.CurrentHealth;
            _player.GetHealth().Maximum = saveData.MaximumHealth;
        }

        _scenes = new List<Scene> {
            new StageOne(_player),
            new StageTwo(_player),
            new StageThree(_player)
        };
        
        _firstStageIndex = _scenes.FindIndex(scene => scene is Stage);
        _lastStageIndex = _scenes.FindLastIndex(scene => scene is Stage);

        if (saveData != null) {
            for (int i = 0; i < _scenes.Count; i++) {
                if (_scenes[i] is Stage stage && stage.GetName() == saveData.CurrentScene) {
                    _currentSceneIndex = i;
                    break;
                }
            }
        } else {
            _currentSceneIndex = _firstStageIndex;
        }

        _currentScene = _scenes[_currentSceneIndex];
        _currentScene.Load();
    }

    public void Unload() {
        _currentScene.Unload();
    }
    
    public void Update(GameTime gameTime) {
        _currentScene.Update(gameTime);

        if (_currentScene is Stage currentStage &&
            currentStage.ReachedNextSceneBounds &&
            _currentSceneIndex < _lastStageIndex) {

            _currentScene.Unload();
            _currentSceneIndex++;
            _currentScene = _scenes[_currentSceneIndex];
            _currentScene.Load();
        }
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        _currentScene.Draw(spriteBatch);
    }
}

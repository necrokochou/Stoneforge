using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Data;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Scenes;
using StoneforgeGame.Game.Scenes.Stages;
using StoneForgeGame.Game.Utilities;


namespace StoneForgeGame.Game.Managers;


public class SceneManager {
    // FIELDS
    private List<Scene> _scenes;
    private int _currentSceneIndex;
    private Scene _currentScene;
    private int _firstStageIndex;
    private int _lastStageIndex;
    
    private Character _player;

    private bool _isFinished;
    private bool _shouldFinish;
    private float _finishTimer;
    private bool _isStartingNewGame;
    private float _startTimer;
    private bool _isLoadingGame;
    private float _loadTimer;
    
    private Rectangle _messageBoxBounds;
    private Color _messageBoxColor = new(0, 0, 0, 200);
    private int _messagePadding = 20;

    private bool _isInMainMenu;
    

    // CONSTRUCTORS
    public SceneManager() {
        _scenes = new List<Scene>();
    }


    // PROPERTIES
    public bool IsInMainMenu {
        get => _isInMainMenu;
    }


    // METHODS'
    public void Load() {
        _scenes = new List<Scene> {
            new MainMenu()
        };
        
        _currentSceneIndex = 0;
        _currentScene = _scenes[_currentSceneIndex];
        _currentScene.Load();
    }

    public void Unload() {
        _currentScene?.Unload();
        _scenes.Clear();
        _currentScene = null;
        _currentSceneIndex = 0;
        _firstStageIndex = 0;
        _lastStageIndex = 0;
        _player = null;
    }
    
    public void Update(GameTime gameTime) {
        _isInMainMenu = _currentScene is MainMenu;
        
        if (_shouldFinish) {
            _finishTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_finishTimer <= 0f) {
                _currentScene.Unload();

                foreach (Scene scene in _scenes) {
                    if (scene is MainMenu menu) {
                        _currentSceneIndex = _scenes.IndexOf(menu);
                        _currentScene = menu;
                        _currentScene.Load();
                        menu.ShowMessage("You completed the game!");
                        menu.Reset();
                        break;
                    }
                }

                _shouldFinish = false;
            }
            return;
        }
        
        if (_isStartingNewGame) {
            _startTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_startTimer <= 0f) {
                SaveManager.DeleteSave();
                
                _player = new Batumbakal();
                _player.ActualPosition = Vector2.Zero;
                
                BuildStages();

                _currentScene.Unload();
                _currentSceneIndex = _firstStageIndex;
                _currentScene = _scenes[_currentSceneIndex];
                _currentScene.Load();

                _isStartingNewGame = false;
            }
            return;
        }

        if (_isLoadingGame) {
            _loadTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_loadTimer <= 0f) {
                SaveData saveData = SaveManager.Load();
                _player = new Batumbakal();
                _player.ActualPosition = new Vector2(saveData.PositionX, saveData.PositionY);

                BuildStages();

                _currentScene.Unload();
                foreach (Scene scene in _scenes) {
                    if (scene is Stage stage) {
                        if (stage.GetName() == saveData.CurrentScene) {
                            _currentSceneIndex = _scenes.IndexOf(stage);
                            break;
                        }
                    }
                }
                _currentScene = _scenes[_currentSceneIndex];
                _currentScene.Load();

                _isLoadingGame = false;
            }
            return;
        }
        
        if (_currentScene is MainMenu && _currentScene.IsFinished) {
            MainMenu menu = (MainMenu) _currentScene;
                
            switch (menu.GetSelection()) {
                case "NewGame":
                    _startTimer = 0.75f;
                    _isStartingNewGame = true;
                    break;

                case "LoadGame": 
                    if (SaveManager.HasSave()) {
                        _loadTimer = 0.75f;
                        _isLoadingGame = true;
                    } else {
                        menu.ShowMessage("No save found.");
                        menu.Reset();
                    }
                    break;

                case "QuitGame":
                    Environment.Exit(0);
                    break;
            }

            return;
        }

        if (_currentScene is Stage) {
            Stage stage = (Stage) _currentScene;
            if (stage.GetObjective() != null &&
                stage.GetObjective().IsCompleted &&
                _currentSceneIndex == _lastStageIndex) {

                _finishTimer = 0.75f;
                _shouldFinish = true;

                _currentScene.Unload();

                foreach (Scene scene in _scenes) {
                    if (scene is MainMenu menu) {
                        _currentSceneIndex = _scenes.IndexOf(scene);
                        _currentScene = menu;
                        _currentScene.Load();
                        break;
                    }
                }
                
                return;
            }
        }
        
        if (_currentScene is Stage) {
            Stage stage = (Stage) _currentScene;
            if (stage != null &&
                stage.GetReachedNextLocation() &&
                _currentSceneIndex < _lastStageIndex) {

                _currentScene.Unload();
                _currentSceneIndex++;
                _currentScene = _scenes[_currentSceneIndex];
                _currentScene.Load();
            }
        }
        
        if (_currentScene is Stage) {
            Stage stage = (Stage) _currentScene;
            if (stage != null &&
                stage.GetObjective() != null &&
                stage.GetObjective().IsDestroyed &&
                _currentSceneIndex < _lastStageIndex) {

                _currentScene.Unload();
                _currentSceneIndex++;
                _currentScene = _scenes[_currentSceneIndex];
                _currentScene.Load();
            }
        }
        
        _currentScene.Update(gameTime);
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        _currentScene.Draw(spriteBatch);
    }

    private void BuildStages() {
        _scenes.Add(new StageOne(_player));
        _scenes.Add(new StageTwo(_player));
        _scenes.Add(new StageThree(_player));
        
        _firstStageIndex = _scenes.FindIndex(scene => scene is Stage);
        _lastStageIndex = _scenes.FindLastIndex(scene => scene is Stage);
    }
}

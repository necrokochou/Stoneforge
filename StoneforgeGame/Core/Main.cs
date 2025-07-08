using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Scenes;
using StoneforgeGame.Game.Scenes.Stages;
using StoneForgeGame.Game.Utilities;
using Color = Microsoft.Xna.Framework.Color;


namespace StoneForgeGame.Core;


public class Main : Microsoft.Xna.Framework.Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private bool _isFullscreen;
    private bool _wasFullscreenKeyPressed;
    
    private SceneManager _sceneManager;
    
    private KeyboardState _kState;
    private KeyboardState _prevKState;

    public Main() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        // _graphics.IsFullScreen = true;
        
        Window.IsBorderless = true;
        Window.Title = "Stoneforge";
        Window.Position = Point.Zero;
        IsMouseVisible = true;
        
        _graphics.ApplyChanges();
    }

    protected override void Initialize() {
        MyDebug.Graphics = GraphicsDevice;
        MyDebug.Window = Window.ClientBounds;
        FontLibrary.Content = Content;
        TextureLibrary.Content = Content;
        AudioLibrary.Content = Content;
        // InputManager.Window = Window.ClientBounds;
        Scene.Window = Window.ClientBounds;
        

        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        FontLibrary.Load();
        TextureLibrary.Load();
        AudioLibrary.Load();
        
        _sceneManager = new SceneManager();
        _sceneManager.Load();
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        _kState = Keyboard.GetState();

        // if (keyState.IsKeyDown(Keys.NumPad0) && !_wasFullscreenKeyPressed) {
        //     ToggleFullscreen();
        // }
        // _wasFullscreenKeyPressed = keyState.IsKeyDown(Keys.NumPad0);

        if (_kState.IsKeyDown(Keys.F1) && _prevKState.IsKeyUp(Keys.F1)) {
            MyDebug.IsDebug = !MyDebug.IsDebug;
        }
        
        _sceneManager.Update(gameTime);
        // if (_sceneManager.IsFinished) {
        //     _sceneManager.Unload();
        //     
        //     Exit();
        // }
        
        _prevKState = _kState;
        
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        
        _sceneManager.Draw(_spriteBatch);
        
        _spriteBatch.End();
        

        base.Draw(gameTime);
    }

    private void ToggleFullscreen() {
        _isFullscreen = !_isFullscreen;
        _graphics.IsFullScreen = _isFullscreen;
        _graphics.ApplyChanges();
    }
}

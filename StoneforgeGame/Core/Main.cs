using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    
    private StageOne _stageOne;

    public Main() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        // _graphics.PreferredBackBufferWidth = 1280;
        // _graphics.PreferredBackBufferHeight = 720;
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        
        Window.Title = "Stoneforge";
        Window.IsBorderless = true;
        Window.Position = Point.Zero;
        
        _graphics.ApplyChanges();
    }

    protected override void Initialize() {
        MyDebug.Graphics = GraphicsDevice;
        Scene.Window = Window.ClientBounds;
        TextureLibrary.Content = Content;
        
        _sceneManager = new SceneManager();
        
        _stageOne = new StageOne();


        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _sceneManager.ChangeScene(_stageOne);
    }

    protected override void Update(GameTime gameTime) {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        KeyboardState keyState = Keyboard.GetState();

        if (keyState.IsKeyDown(Keys.NumPad0) && !_wasFullscreenKeyPressed) {
            ToggleFullscreen();
        }
        _wasFullscreenKeyPressed = keyState.IsKeyDown(Keys.NumPad0);
        
        _sceneManager.Update(gameTime);
        
        
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

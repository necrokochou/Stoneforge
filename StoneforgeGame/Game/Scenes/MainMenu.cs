using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Scenes.Components;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Scenes;


public class MainMenu : Scene {
    // FIELDS
    private string _selection;
    private Rectangle _newGameButton;
    private Rectangle _loadGameButton;
    private Rectangle _quitGameButton;

    private MouseState _prevMState;
    
    private string _notificationMessage;
    private float _notificationTimer;

    private Rectangle _messageBoxBounds;
    private Color _messageBoxColor = new(0, 0, 0, 200);
    private int _messagePadding = 20;



    // CONSTRUCTORS
    public MainMenu() {
        Background = new Background(TextureLibrary.MenuBackground, Window.Size);
        _newGameButton = new Rectangle(700, 552, 515, 124);
        _loadGameButton = new Rectangle(700, 697, 515, 124);
        _quitGameButton = new Rectangle(700, 842, 515, 124);
    }


    // PROPERTIES
    


    // METHODS
    public override void Load() { }

    public override void Unload() {
        Background = null;

        _selection = null;
        _prevMState = default;
    }

    public override void Update(GameTime gameTime) {
        MouseState mState = Mouse.GetState();
        
        if (_notificationTimer > 0f) {
            _notificationTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (_notificationTimer <= 0f) {
                _notificationMessage = null;
            }
        }

        if (mState.LeftButton == ButtonState.Pressed &&
            _prevMState.LeftButton == ButtonState.Released) {

            if (_newGameButton.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed) {
                _selection = "NewGame";
                IsFinished = true;
            } else if (_loadGameButton.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed) {
                _selection = "LoadGame";
                IsFinished = true;
            } else if (_quitGameButton.Contains(mState.Position) && mState.LeftButton == ButtonState.Pressed) {
                _selection = "QuitGame";
                IsFinished = true;
            }
        }
        
        _prevMState = mState;
    }

    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        
        spriteBatch.Draw(MyDebug.Texture, _newGameButton, Color.Red * 0.5f);
        spriteBatch.Draw(MyDebug.Texture, _loadGameButton, Color.Red * 0.5f);
        spriteBatch.Draw(MyDebug.Texture, _quitGameButton, Color.Red * 0.5f);
        
        if (!string.IsNullOrEmpty(_notificationMessage)) {
            DrawMessageBox(spriteBatch, _notificationMessage);
        }
    }

    private void DrawMessageBox(SpriteBatch spriteBatch, string message) {
        SpriteFont font = FontLibrary.TempFont;
        Vector2 textSize = font.MeasureString(message);

        _messageBoxBounds = new Rectangle(
            (int)((Window.Width - textSize.X) / 2) - _messagePadding,
            520,
            (int)textSize.X + (_messagePadding * 2),
            (int)textSize.Y + (_messagePadding * 2)
        );

        Vector2 textPosition = new(
            _messageBoxBounds.X + _messagePadding,
            _messageBoxBounds.Y + _messagePadding
        );

        spriteBatch.Draw(MyDebug.Texture, _messageBoxBounds, _messageBoxColor);
        spriteBatch.DrawString(font, message, textPosition, Color.Yellow);
    }
    
    public void Reset() {
        _selection = null;
        IsFinished = false;
        _prevMState = default;

        _notificationMessage = null;
        _notificationTimer = 0f;
    }
    
    public void ShowMessage(string message, float duration = 2f) {
        _notificationMessage = message;
        _notificationTimer = duration;
    }


    public string GetSelection() {
        return _selection;
    }
}
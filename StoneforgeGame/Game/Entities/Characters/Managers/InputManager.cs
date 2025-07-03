using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace StoneforgeGame.Game.Entities.Characters.Managers;


public class InputManager {
    // FIELDS
    private KeyboardState _keyState;
    private MouseState _mouseState;

    private bool _moveLeft;
    private bool _moveRight;
    private bool _moveJump;

    private bool _reset;
    private bool _teleport;


    // CONSTRUCTORS



    // PROPERTIES
    public bool MoveLeft {
        get => _moveLeft;
    }
    public bool MoveRight {
        get => _moveRight;
    }
    public bool MoveJump {
        get => _moveJump;
    }
    
    public bool Reset {
        get => _reset;
    }
    public bool Teleport {
        get => _teleport;
    }
    public Point TeleportLocation {
        get => _mouseState.Position;
    }


    // METHODS
    public void Update() {
        _keyState = Keyboard.GetState();
        _mouseState = Mouse.GetState();
        
        _moveLeft = _keyState.IsKeyDown(Keys.A);
        _moveRight = _keyState.IsKeyDown(Keys.D);
        _moveJump = _keyState.IsKeyDown(Keys.Space);
        
        _reset = _keyState.IsKeyDown(Keys.R);
        _teleport = _mouseState.LeftButton == ButtonState.Pressed;
    }
}

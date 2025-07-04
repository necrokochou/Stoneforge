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
    private bool _pressJump;
    private bool _releaseJump;
    private bool _pressAttack;
    private bool _releaseAttack;
    
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
    public bool PressJump {
        get => _pressJump;
    }
    public bool ReleaseJump {
        get => _releaseJump;
    }
    public bool PressAttack {
        get => _pressAttack;
    }
    public bool ReleaseAttack {
        get => _releaseAttack;
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
        _pressJump = _keyState.IsKeyDown(Keys.Space);
        _releaseJump = _keyState.IsKeyUp(Keys.Space);
        _pressAttack = _mouseState.LeftButton == ButtonState.Pressed;
        _releaseAttack = _mouseState.LeftButton == ButtonState.Released;
        
        _reset = _keyState.IsKeyDown(Keys.R);
        _teleport = _keyState.IsKeyDown(Keys.T);
    }
}

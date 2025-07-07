using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Entities.ObjectTiles;


public abstract class ObjectTile {
    // FIELDS
    protected Texture Texture;
    public Rectangle Destination;
    protected Rectangle Source;
    protected Color Color;

    protected Point Size;

    protected Stage Stage;
    
    protected BoxCollider CollisionBox;

    // protected Vector2 ActualPosition;
    
    // protected AnimationManager AnimationManager;

    public bool IsDestroyable;
    public bool IsDestroyed;
    
    private static ObjectTile _currentlyDraggedTile;
    private bool _isDragging;
    private Vector2 _dragOffset;


    // CONSTRUCTORS
    


    // PROPERTIES

    

    // METHODS
    public abstract void Load(Point location);
    
    public virtual void Update() { }
    
    public abstract void Draw(SpriteBatch spriteBatch);

    public virtual BoxCollider GetCollisionBox() {
        return CollisionBox;
    }
    
    public virtual void OnDestroy() { }

    protected virtual void Destroy() {
        IsDestroyed = true;
    }
    
    public void DebugDragTile() {
        KeyboardState keyState = Keyboard.GetState();
        MouseState mouseState = Mouse.GetState();
        Vector2 mousePosition = mouseState.Position.ToVector2();

        if (keyState.IsKeyDown(Keys.LeftShift) && mouseState.LeftButton == ButtonState.Pressed) {
            if (CollisionBox.Bounds.Contains(mouseState.Position.ToVector2())) {
            if (!_isDragging && (_currentlyDraggedTile == null || _currentlyDraggedTile == this)) {
                _isDragging = true;
                _dragOffset = mousePosition - Destination.Location.ToVector2();
                _currentlyDraggedTile = this;
            }
        }
    } else {
        if (_currentlyDraggedTile == this) {
            _currentlyDraggedTile = null;
        }
        _isDragging = false;
    }

    if (_isDragging && _currentlyDraggedTile == this) {
        Vector2 newPosition = mousePosition - _dragOffset;
        Destination = new Rectangle(newPosition.ToPoint(), Size);
        CollisionBox.Move(newPosition.ToPoint(), newPosition.ToPoint() + Size);
        Console.WriteLine(CollisionBox.Bounds.Location);
    }
        
    }
}

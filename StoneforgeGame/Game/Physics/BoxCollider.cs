using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Physics;


public class BoxCollider {
    // FIELDS
    private Rectangle _destination;
    private Rectangle _nextHorizontalDestination;
    private Rectangle _nextVerticalDestination;
    private Color _color;

    private bool _isSolid;
    private bool _isDamage;

    private Character _owner;


    // CONSTRUCTORS
    public BoxCollider(Point location, Point size, bool isSolid = true, bool isDamage = false, Character owner = null) {
        _destination = new Rectangle(location.X, location.Y,
            size.X - location.X, size.Y - location.Y
        );
        _isSolid = isSolid;
        _isDamage = isDamage;
        _owner = owner;

        _color = Color.Red;
    }

    // PROPERTIES
    public Rectangle Bounds {
        get => _destination;
    }
    public Rectangle NextHorizontalBounds {
        get => _nextHorizontalDestination;
    }
    public Rectangle NextVerticalBounds {
        get => _nextVerticalDestination;
    }
    public bool IsDamage {
        get => _isDamage;
    }


    // METHODS
    public void Update(Rectangle destination) {
        _destination = destination;
    }
    
    public void Draw(SpriteBatch spriteBatch, int thickness) {
        MyDebug.DrawHollowRect(spriteBatch, _destination, _color, thickness);
    }
    
    public void GetNextBounds(Vector2 position, Vector2 nextPosition) {
        _nextHorizontalDestination = new Rectangle(
            (int) nextPosition.X,
            (int) position.Y,
            _destination.Width,
            _destination.Height
        );
        
        _nextVerticalDestination = new Rectangle(
            (int) position.X,
            (int) nextPosition.Y,
            _destination.Width,
            _destination.Height
        );
    }

    public bool HasCollided(CollisionManager collisionManager, Rectangle nextIntendedBounds) {
        foreach (BoxCollider collider in collisionManager.Colliders) {
            if (this != collider && collider.Bounds.Intersects(nextIntendedBounds)) {
                if (_owner != null) {
                    if (_owner.CanGetHit && collider._isDamage) {
                        _owner.AttrHealth.Decrease(5);
                    }
                }
                
                if (collider._isSolid)
                    return true;
            }
        }

        return false;
    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Physics;


public class BoxCollider {
    // FIELDS
    private Rectangle _destination;
    private Rectangle _nextHorizontalDestination;
    private Rectangle _nextVerticalDestination;
    private Color _color;

    private Character _owner;
    private bool _hasOwner;

    private float _collisionDamage;

    private bool _isSolid;
    private bool _isDamage;



    // CONSTRUCTORS
    public BoxCollider(Point start, Point end, bool solid = true, bool damage = false, Character owner = null) {
        _destination = new Rectangle(
            start, end - start
        );
        
        _isSolid = solid;
        _isDamage = damage;
        _owner = owner;
        _hasOwner = owner != null;

        _color = Color.Red;

        _collisionDamage = 10;
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
    public Character Owner {
        get => _owner;
    }
    public bool HasOwner {
        get => _hasOwner;
    }
    public float CollisionDamage {
        get => _collisionDamage;
    }
    public bool IsSolid {
        get => _isSolid;
    }
    public bool IsDamage {
        get => _isDamage;
    }


    // METHODS
    public void Update(Rectangle destination) {
        _destination = new Rectangle(
            destination.Location, destination.Size
        );
    }
    
    public void Draw(SpriteBatch spriteBatch, int thickness) {
        MyDebug.DrawHollowRect(spriteBatch, _destination, Color.Yellow, thickness);
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
        bool collided = false;
        bool damaged = false;
        
        foreach (BoxCollider collider in collisionManager.Colliders) {
            if (this == collider || !collider.Bounds.Intersects(nextIntendedBounds)) continue;

            if (!damaged && _owner?.CanGetHit == true && collider._isDamage) {
                _owner.CurrentHealth.Decrease(collider._collisionDamage);
                damaged = true;
            }

            if (collider._isSolid) {
                collided = true;
            }
        }

        return collided;
    }
}

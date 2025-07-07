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

    private Vector2 _offsetRatio;

    private Character _owner;
    private bool _hasOwner;

    private float _collisionDamage;

    private bool _isSolid;
    private bool _isDamage;



    // CONSTRUCTORS
    public BoxCollider(Point start, Point end, Vector2 offsetRatio = default, bool solid = true, bool damage = false, Character owner = null) {
        _destination = new Rectangle(
            start,
            end - start
        );

        _offsetRatio = offsetRatio == default ? new Vector2(1f, 1f) : offsetRatio;
        
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
    public Vector2 OffsetRatio {
        get => _offsetRatio;
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
    public void Update() {
        int colliderWidth = (int) (_owner.Bounds.Width * _offsetRatio.X);
        int colliderHeight = (int) (_owner.Bounds.Height * _offsetRatio.Y);

        int colliderX = (int) (_owner.ActualPosition.X + (_owner.Bounds.Width - colliderWidth) * _offsetRatio.X);
        int colliderY = (int) (_owner.ActualPosition.Y + (_owner.Bounds.Height - colliderHeight) * _offsetRatio.Y);

        _destination = new Rectangle(
            colliderX,
            colliderY,
            colliderWidth,
            colliderHeight
        );
    }

    public void Move(Point start, Point end) {
        _destination = new Rectangle(
            start, end - start
        );
    }
    
    public void Draw(SpriteBatch spriteBatch, int thickness) {
        MyDebug.DrawHollowRect(spriteBatch, _destination, Color.Yellow, thickness);
    }
    
    public void GetNextBounds(Vector2 position, Vector2 nextPosition) {
        int colliderWidth = _destination.Width;
        int colliderHeight = _destination.Height;

        int nextX = (int) (nextPosition.X + (_owner.Bounds.Width - colliderWidth) * _offsetRatio.X);
        int currentY = (int) (position.Y + (_owner.Bounds.Height - colliderHeight) * _offsetRatio.Y);

        _nextHorizontalDestination = new Rectangle(
            nextX,
            currentY,
            colliderWidth,
            colliderHeight
        );

        int currentX = (int) (position.X + (_owner.Bounds.Width - colliderWidth) * _offsetRatio.X);
        int nextY = (int) (nextPosition.Y + (_owner.Bounds.Height - colliderHeight) * _offsetRatio.Y);

        _nextVerticalDestination = new Rectangle(
            currentX,
            nextY,
            colliderWidth,
            colliderHeight
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

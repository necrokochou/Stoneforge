using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Physics;


public class BoxCollider {
    // FIELDS
    private Rectangle _destination;
    private Rectangle _nextHorizontalDestination;
    private Rectangle _nextVerticalDestination;


    // CONSTRUCTORS
    public BoxCollider(Point location, Point size) {
        _destination = new Rectangle(location.X, location.Y,
            size.X - location.X, size.Y - location.Y
        );
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


    // METHODS
    public void Update(Rectangle destination) {
        _destination = destination;
    }
    
    public void Draw(SpriteBatch spriteBatch, Color color, int thickness) {
        MyDebug.DrawHollowRect(spriteBatch, _destination, color, thickness);
    }
    
    public void GetNextBoundsX(Vector2 position, float deltaX) {
        _nextHorizontalDestination = new Rectangle(
            (int) (position.X + deltaX),
            (int) position.Y,
            _destination.Width,
            _destination.Height
        );
    }

    public void GetNextBoundsY(Vector2 position, float deltaY) {
        _nextVerticalDestination = new Rectangle(
            (int) position.X,
            (int) (position.Y + deltaY),
            _destination.Width,
            _destination.Height
        );
    }

    public bool HasCollided(CollisionManager collisionManager, Rectangle nextIntendedBounds) {
        foreach (BoxCollider collider in collisionManager.Colliders) {
            if (this != collider && collider.Bounds.Intersects(nextIntendedBounds)) {
                return true;
            }
        }

        return false;
    }
}

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
                return true;
            }
        }

        return false;
    }
}

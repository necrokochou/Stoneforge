using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Managers;


public class CollisionManager {
    // FIELDS
    private List<BoxCollider> _colliders = new List<BoxCollider>();


    // CONSTRUCTORS



    // PROPERTIES
    public List<BoxCollider> Colliders {
        get => _colliders;
    }


// METHODS
    public void Unload() {
        _colliders.Clear();
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        foreach (BoxCollider collider in _colliders) {
            MyDebug.DrawRect(spriteBatch, collider.Bounds, Color.Red * 0.3f);
        }
    }

    public void Add(BoxCollider boxCollider) {
        _colliders.Add(boxCollider);
    }
    
    public void Add(Point start, Point end) {
        BoxCollider newBoxCollider = new BoxCollider(start, end);
        
        _colliders.Add(newBoxCollider);
    }

    public void SetBorder(int thickness = 1) {
        int borderThickness = 1 * thickness;
        
        BoxCollider topBorder = new BoxCollider(
            new Point(0, 0),
            new Point(Scene.Window.Size.X, 0 + borderThickness));
        BoxCollider bottomBorder = new BoxCollider(
            new Point(0, Scene.Window.Size.Y - borderThickness),
            new Point(Scene.Window.Size.X, Scene.Window.Size.Y)
        );
        BoxCollider leftBorder = new BoxCollider(
            new Point(0, 0),
            new Point(0 + borderThickness, Scene.Window.Size.Y));
        BoxCollider rightBorder = new BoxCollider(
            new Point(Scene.Window.Size.X - borderThickness, 0),
            new Point(Scene.Window.Size.X, Scene.Window.Size.Y));

        _colliders.Add(topBorder);
        _colliders.Add(bottomBorder);
        _colliders.Add(leftBorder);
        _colliders.Add(rightBorder);
    }

    // public static float CheckCollision(float velocity, float deltaTime, Rectangle nextIntendedBounds) {
    //     if (!HasCollided(nextIntendedBounds)) {
    //         return velocity * deltaTime;
    //     }
    //
    //     return 0;
    // }
}

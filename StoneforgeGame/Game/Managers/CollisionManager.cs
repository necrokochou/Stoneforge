﻿using System;
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
    
    public void Add(Point start, Point end, bool isSolid = true, bool isDamage = false, bool ignore = false) {
        BoxCollider newBoxCollider = new BoxCollider(start, end, isSolid, isDamage);
        foreach (BoxCollider collider in _colliders) {
            if (collider.Bounds.Intersects(newBoxCollider.Bounds) && !ignore)
                throw new ArgumentException(
                    $"Box collider {newBoxCollider.Bounds.Location} {newBoxCollider.Bounds.Size}"
                    + $"intersects with another box collider {collider.Bounds.Location} {collider.Bounds.Size}"
                );
        }
        
        _colliders.Add(newBoxCollider);
    }

    public void AddSquare(Point start, int width, int height, bool isSolid = true, bool isDamage = false) {
        Add(start, new Point(start.X + width, start.Y + height), isSolid, isDamage);
    }

    public void SetBorder(int thickness = 0, bool top = false, bool bottom = false, bool left = false, bool right = false, bool all = false) {
        BoxCollider topBorder = new BoxCollider(
            new Point(0, 0),
            new Point(Scene.Window.Size.X, 0 + thickness)
        );
        BoxCollider bottomBorder = new BoxCollider(
            new Point(0, Scene.Window.Size.Y - thickness),
            new Point(Scene.Window.Size.X, Scene.Window.Size.Y)
        );
        BoxCollider leftBorder = new BoxCollider(
            new Point(0, 0),
            new Point(0 + thickness, Scene.Window.Size.Y)
        );
        BoxCollider rightBorder = new BoxCollider(
            new Point(Scene.Window.Size.X - thickness, 0),
            new Point(Scene.Window.Size.X, Scene.Window.Size.Y)
        );

        if (all) {
            _colliders.Add(topBorder);
            _colliders.Add(bottomBorder);
            _colliders.Add(leftBorder);
            _colliders.Add(rightBorder);
            return;
        }
        
        if (top) _colliders.Add(topBorder);
        if (bottom) _colliders.Add(bottomBorder);
        if (left) _colliders.Add(leftBorder);
        if (right) _colliders.Add(rightBorder);
    }

    // public static float CheckCollision(float velocity, float deltaTime, Rectangle nextIntendedBounds) {
    //     if (!HasCollided(nextIntendedBounds)) {
    //         return velocity * deltaTime;
    //     }
    //
    //     return 0;
    // }
}

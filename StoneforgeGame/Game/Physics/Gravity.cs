using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Entities.Characters;


namespace StoneforgeGame.Game.Physics;


public class Gravity {
    // FIELDS
    private float _magnitude;
    private Vector2 _direction;


    // CONSTRUCTORS
    public Gravity(float magnitude, Vector2 direction) {
        _magnitude = magnitude;
        _direction = direction;
    }


    // PROPERTIES
    public float Magnitude {
        get => _magnitude;
    }
    public Vector2 Direction {
        get => _direction;
    }


    // METHODS
}

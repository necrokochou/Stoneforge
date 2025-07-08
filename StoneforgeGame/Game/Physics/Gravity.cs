using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Entities.Characters;


namespace StoneforgeGame.Game.Physics;


public class Gravity {
    // FIELDS
    private float _magnitude;
    private Vector2 _direction;
    private Vector2 _force;


    // CONSTRUCTORS
    public Gravity(float magnitude, Vector2 direction) {
        _magnitude = magnitude;
        _direction = direction;
        
        _force = _direction * _magnitude;
    }


    // PROPERTIES
    public Vector2 Force {
        get => _force;
    }


    // METHODS
    public void Apply(Character character, float deltaTime) {
        
    }
}

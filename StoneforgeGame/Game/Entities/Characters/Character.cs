using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Graphics;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using Texture = StoneforgeGame.Game.Utilities.Texture;


namespace StoneforgeGame.Game.Entities.Characters;


public abstract class Character {
    // FIELDS
    protected Texture Texture;
    protected Rectangle Source;
    protected Rectangle Destination;
    protected Color Color;
    
    protected string Name;
    protected BoxCollider CollisionBox;

    protected float WalkSpeed;
    protected float JumpPower;
    
    protected Point GamePosition;
    protected Vector2 ActualPosition;
    protected Vector2 Velocity;
    protected Point Direction;

    protected Animation CurrentAnimation;
    protected Animation PreviousAnimation;
    protected Dictionary<string,Animation> Animations = new Dictionary<string, Animation>();

    // CONSTRUCTORS
    
    

    // PROPERTIES
    public BoxCollider Collider {
        get => CollisionBox;
    }


    // METHODS
    public abstract void Load(Point position, int sizeMultiplier = 1);

    public abstract void Update(GameTime gameTime, CollisionManager collisionManager, Gravity gravity);

    public abstract void Draw(SpriteBatch spriteBatch);

    protected void ApplyVelocity(Vector2 velocity, Vector2 direction, Gravity gravity) { }
}

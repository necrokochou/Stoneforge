using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Graphics;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using Texture = StoneforgeGame.Game.Graphics.Texture;


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
    protected int JumpCount;
    
    protected Point GamePosition;
    protected Vector2 ActualPosition;
    protected Vector2 NextPosition;
    protected Vector2 Velocity;
    protected Vector2 Direction;

    protected AnimationManager AnimationManager;

    protected bool IsFacingRight;
    protected bool IsOnGround;
    protected bool IsIdle;
    protected bool IsJumping;
    protected bool IsFalling;
    protected bool IsWalking;

    // CONSTRUCTORS
    
    

    // PROPERTIES
    public BoxCollider Collider {
        get => CollisionBox;
    }


    // METHODS
    public abstract void Load(Point position, int sizeMultiplier = 1);

    public abstract void Update(GameTime gameTime, CollisionManager collisionManager, Gravity gravity);

    public abstract void Draw(SpriteBatch spriteBatch);

    // protected void ApplyVelocity(Vector2 velocity, Vector2 direction, Gravity gravity) { }

    protected void CheckState() {
        IsJumping = Velocity.Y < 0 && !IsOnGround;
        IsFalling = Velocity.Y > 0 && !IsOnGround;
        IsWalking = Direction.X != 0 && IsOnGround;
        IsIdle = IsOnGround && !IsWalking && !IsJumping && !IsFalling;
        
        if (IsIdle) AnimationManager.Play("Idle");
        else if (IsJumping) AnimationManager.Play("Jump");
        else if (IsFalling) AnimationManager.Play("Fall");
        else if (IsWalking) AnimationManager.Play("Walk");
    }
}

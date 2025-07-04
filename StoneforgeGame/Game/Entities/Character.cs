using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Entities;


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
    protected float AttackSpeed;
    protected float AttackCooldown;
    protected float AttackCooldownTimer;
    
    protected Vector2 GamePosition;
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
    protected bool IsAttacking;

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
        IsIdle = IsOnGround && !IsWalking && !IsJumping && !IsFalling && !IsAttacking;
        
        if (IsAttacking) AnimationManager.Play("Attack");
        else if (IsJumping) AnimationManager.Play("Jump");
        else if (IsFalling) AnimationManager.Play("Fall");
        else if (IsWalking) AnimationManager.Play("Walk");
        else if (IsIdle) AnimationManager.Play("Idle");
    }
    
    protected void CheckCollision(CollisionManager collisionManager) {
        if (!CollisionBox.HasCollided(collisionManager, CollisionBox.NextHorizontalBounds)) {
            ActualPosition.X = NextPosition.X;
        } else {
            Velocity.X = 0;
        }
        
        if (!CollisionBox.HasCollided(collisionManager, CollisionBox.NextVerticalBounds)) {
            ActualPosition.Y = NextPosition.Y;
            IsOnGround = false;
        } else {
            Velocity.Y = 0;
            IsOnGround = true;
            IsJumping = false;
            JumpCount = 2;
        }
    }

    protected void UpdateGamePos() {
        GamePosition = new Vector2(ActualPosition.X + (float) Source.X / 2, ActualPosition.Y + Source.Y);
        
        // Console.WriteLine(GamePosition);
    }
}

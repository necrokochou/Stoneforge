using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Attributes;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Entities.Characters;


public abstract class Character {
    // FIELDS
    protected Texture Texture;
    protected Rectangle Destination;
    protected Rectangle Source;
    protected Color Color;
    
    protected string Name;
    protected Health Health;
    
    protected BoxCollider CollisionBox;

    protected float WalkSpeed;
    protected float JumpPower;
    protected int JumpCount;
    protected float InvincibilityFrames;
    protected float InvincibilityTimer;
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
    protected bool IsInvincible;
    protected bool IsIdle;
    protected bool IsJumping;
    protected bool IsFalling;
    protected bool IsWalking;
    protected bool IsHit;
    protected bool IsAttacking;

    // CONSTRUCTORS
    
    

    // PROPERTIES
    public Health AttrHealth {
        get => Health;
    }
    public BoxCollider Collider {
        get => CollisionBox;
    }
    public bool CanGetHit {
        get => !IsInvincible;
    }


    // METHODS
    public abstract void Load(Rectangle window, Point position, int sizeMultiplier = 1);

    public abstract void Update(GameTime gameTime, CollisionManager collisionManager, Gravity gravity);

    public abstract void Draw(SpriteBatch spriteBatch);

    // protected void ApplyVelocity(Vector2 velocity, Vector2 direction, Gravity gravity) { }

    protected void CheckState() {
        IsInvincible = InvincibilityTimer > 0f;
        IsJumping = Velocity.Y < 0 && !IsOnGround;
        IsFalling = Velocity.Y > 0 && !IsOnGround;
        IsWalking = Direction.X != 0 && IsOnGround;
        IsIdle = IsOnGround && !IsWalking && !IsJumping && !IsFalling && !IsAttacking;
        
        if (IsHit) AnimationManager.Play("Hit");
        else if (IsAttacking) AnimationManager.Play("Attack");
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

    protected void DrawAttributes(SpriteBatch spriteBatch) {
        Health.Draw(spriteBatch);
    }
}

using System.Collections.Generic;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Attributes;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;
using Texture = StoneforgeGame.Game.Graphics.Texture;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace StoneforgeGame.Game.Entities.Characters;


public abstract class Character {
    // FIELDS
    protected Texture Texture;
    protected Rectangle Destination;
    protected Rectangle Source;
    protected Color Color;
    
    protected string Name;
    protected Health Health;
    public string UniqueID;
    
    protected BoxCollider CollisionBox;
    protected Rectangle MeleeRange;

    protected float WalkSpeed;
    protected float JumpPower;
    protected int JumpCount;
    protected float InvincibilityFrames;
    protected float InvincibilityTimer;
    protected float AttackDamage;
    protected float AttackSpeed;
    protected float AttackCooldown;
    protected float AttackCooldownTimer;

    protected int GemCount;
    
    protected Vector2 GamePosition;
    public Vector2 ActualPosition;
    protected Vector2 NextPosition;
    public Vector2 PreviousPosition;
    protected Vector2 Velocity;
    protected Vector2 Direction;

    protected AnimationManager AnimationManager;

    public bool IsFacingRight;
    protected bool IsOnGround;
    protected bool IsInvincible;
    protected bool IsIdle;
    protected bool IsJumping;
    protected bool IsFalling;
    protected bool IsWalking;
    protected bool IsHit;
    protected bool IsAttacking;
    public bool IsAlive;

    protected bool CanMove = true;
    protected bool CanJump = true;

        
    // CONSTRUCTORS
    
    

    // PROPERTIES
    public Rectangle Bounds {
        get => Destination;
    }
    public Health CurrentHealth {
        get => Health;
    }
    public bool CanGetHit {
        get => !IsInvincible;
    }


    // METHODS
    public abstract void Load(Point location);

    public abstract void Update(GameTime gameTime, Stage stage, Gravity gravity);

    public abstract void Draw(SpriteBatch spriteBatch);

    protected abstract void CheckState();
    
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
        }
    }

    protected void CheckMeleeRange(Stage stage) {
        HitCharacter(stage);
        UniqueDestroyTile(stage);
    }
    
    private void HitCharacter(Stage stage) {
        var charactersToHit = new List<Character>();
        
        foreach (Character character in stage.GetCharacterManager.Characters) {
            if (this == character) continue;

            if (character != null &&
                character.IsAlive &&
                character.GetCollisionBox().Bounds.Intersects(MeleeRange)) {
                
                charactersToHit.Add(character);
            }
        }
        
        foreach (Character character in charactersToHit) {
            if (character.IsAlive) {
                character.Health.Current -= AttackDamage;
            }
        }
    }
    
    protected virtual void UniqueDestroyTile(Stage stage) { }
    protected virtual void UniqueInteract(Stage stage) { }

    protected void DrawAttributes(SpriteBatch spriteBatch) {
        Health.Draw(spriteBatch);
    }

    public Health GetHealth() {
        return Health;
    }

    public BoxCollider GetCollisionBox() {
        return CollisionBox;
    }

    public int GetGemCount() {
        return GemCount;
    }
}

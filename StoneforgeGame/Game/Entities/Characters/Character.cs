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
    
    protected Vector2 GamePosition;
    public Vector2 ActualPosition;
    protected Vector2 NextPosition;
    public Vector2 PreviousPosition;
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
    protected bool IsAlive;
    public bool IsDead;

    protected bool CanDoAnything;
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

    // protected void ApplyVelocity(Vector2 velocity, Vector2 direction, Gravity gravity) { }

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
        var objectTilesToDestroy = new List<ObjectTile>();
        var charactersToHit = new List<Character>();

        // Iterate through all object tiles in the stage
        foreach (ObjectTile objectTile in stage.GetObjectTileManager.ObjectTiles) {
            if (objectTile != null &&
                objectTile.IsDestroyable &&
                objectTile.GetCollisionBox().Bounds.Intersects(MeleeRange)) {
                
                objectTilesToDestroy.Add(objectTile);
            }
        }
        
        // Iterate through all characters except player in the stage
        foreach (Character character in stage.GetCharacterManager.Characters) {
            if (this == character) continue;

            if (character != null &&
                character.IsAlive &&
                character.GetCollisionBox().Bounds.Intersects(MeleeRange)) {
                
                charactersToHit.Add(character);
            }
        }

        // Destroy all object tiles in melee range
        foreach (ObjectTile objectTile in objectTilesToDestroy) {
            stage.GetCollisionManager.Remove(objectTile.GetCollisionBox());
            objectTile.OnDestroy();
            stage.GetObjectTileManager.Remove(objectTile);
        }
        
        // Hit all characters in melee range
        foreach (Character character in charactersToHit) {
            character.Health.Current -= AttackDamage;
        }
    }

    // protected void UpdateGamePos() {
    //     GamePosition = new Vector2(ActualPosition.X + (float) Source.X / 2, ActualPosition.Y + Source.Y);
    //     
    //     // Console.WriteLine(GamePosition);
    // }

    public Health GetHealth() {
        return Health;
    }

    public BoxCollider GetCollisionBox() {
        return CollisionBox;
    }

    protected void DrawAttributes(SpriteBatch spriteBatch) {
        Health.Draw(spriteBatch);
    }
}

using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters.Managers;
using StoneforgeGame.Game.Graphics;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace StoneforgeGame.Game.Entities.Characters;


public class Batumbakal : Character {
    // FIELDS
    private InputManager _input = new InputManager();
    
    private bool _isHoldingJump;

    private Point _origin;


    // CONSTRUCTORS
    public Batumbakal() {
        Name = "Batumbakal";
        Texture = TextureLibrary.Batumbakal;
        WalkSpeed = 200f;
        JumpPower = 700f;
        JumpCount = 2;
        AttackCooldown = 3f;
        
        IsFacingRight = true;
    }


    // PROPERTIES
    private Animation CurrentAnimation {
        get => AnimationManager.CurrentAnimation;
    }


    // METHODS
    public override void Load(Point position, int sizeMultiplier = 1) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        Source = new Rectangle(
            frameWidth * 0, frameHeight * 0,
            frameWidth, frameHeight
        );
        Destination = new Rectangle(
            position.X, position.Y,
            frameWidth * sizeMultiplier, frameHeight * sizeMultiplier
        );
        Color = Color.White;

        CollisionBox = new BoxCollider(
            Destination.Location,
            Destination.Size - Destination.Location
        );

        _origin = position;
        ActualPosition = position.ToVector2();
        
        AnimationManager = new AnimationManager(AnimationLibrary.BatumbakalAnimations);
        AnimationManager.Play("Idle");
    }

    public override void Update(GameTime gameTime, CollisionManager collisionManager, Gravity gravity) { 
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        _input.Update();

        #region --- DEBUGGING ---
        if (_input.Reset) {
            ActualPosition = _origin.ToVector2();
        }
        
        if (_input.Teleport) {
            Velocity = Vector2.Zero;
            ActualPosition = _input.TeleportLocation.ToVector2();
        }
        #endregion
        
        #region --- MOVEMENT ---
        Direction = Vector2.Zero;
        if (_input.MoveLeft) {
            Direction.X -= 1;
            IsFacingRight = false;
        } else if (_input.MoveRight) {
            Direction.X += 1;
            IsFacingRight = true;
        }
        #endregion
        
        #region --- JUMP ---
        if (_input.PressJump && JumpCount > 0 && !_isHoldingJump) {
            Velocity.Y = -JumpPower;
            _isHoldingJump = true;
            JumpCount--;
            Direction.Y = -1;
        }

        if (_input.ReleaseJump) {
            _isHoldingJump = false;
        }
        #endregion

        #region --- ATTACK ---
        if (_input.PressAttack && !IsAttacking && AttackCooldownTimer <= 0f) {
            IsAttacking = true;
            AnimationManager.Play("Attack");
            AttackCooldownTimer = AttackCooldown;
        }
        
        if (IsAttacking && CurrentAnimation.IsFinished) {
            IsAttacking = false;
        }

        if (!IsAttacking && AttackCooldownTimer > 0f) {
            AttackCooldownTimer -= deltaTime;
            if (AttackCooldownTimer < 0f) AttackCooldownTimer = 0f;
        }
        #endregion
        
        CheckState();

        #region --- VELOCITY ---
        Velocity.X = Direction.X * WalkSpeed;
        Velocity.Y += gravity.Magnitude * deltaTime;
        if (IsAttacking) {
            Velocity *= 0.2f;
        }
        #endregion

        #region --- COLLISION ---
        NextPosition.X = ActualPosition.X + Velocity.X * deltaTime;
        NextPosition.Y = ActualPosition.Y + Velocity.Y * deltaTime;
        CollisionBox.GetNextBounds(ActualPosition, NextPosition);
        CheckCollision(collisionManager);
        #endregion
        
        #region --- POSITION ---
        Destination.Location = ActualPosition.ToPoint();
        UpdateGamePos();
        CollisionBox.Update(Destination);
        #endregion
        
        AnimationManager.Update();
        // Console.WriteLine(AttackCooldownTimer);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        SpriteEffects flip = IsFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        CollisionBox.Draw(spriteBatch, Color.Yellow * 0.5f, 2);
        spriteBatch.Draw(
            Texture.Image,
            Destination,
            AnimationManager.GetFrame(Source.Width, Source.Height),
            Color,
            0f,
            Vector2.Zero,
            flip,
            0f
        );
    }
}

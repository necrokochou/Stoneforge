using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Attributes;
using StoneforgeGame.Game.Graphics;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;
using StoneforgeGame.Game.Utilities;
using StoneForgeGame.Game.Utilities;
using Color = Microsoft.Xna.Framework.Color;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Vector2 = Microsoft.Xna.Framework.Vector2;


namespace StoneforgeGame.Game.Entities.Characters;


public class Batumbakal : Character {
    // FIELDS
    private InputManager _input = new InputManager();

    private int _gems;
    
    private bool _isHoldingJump;

    private Point _origin;


    // CONSTRUCTORS
    public Batumbakal() {
        Texture = TextureLibrary.Batumbakal;
        
        Name = "Batumbakal";
        Health = new Health(50);
        
        WalkSpeed = 200f;
        JumpPower = 550f;
        JumpCount = 2;
        InvincibilityFrames = 3f;
        AttackDamage = 20f;
        AttackCooldown = 1f;
        
        IsFacingRight = true;
    }


    // PROPERTIES
    private Animation CurrentAnimation {
        get => AnimationManager.CurrentAnimation;
    }


    // METHODS
    public override void Load(Point location) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        Source = new Rectangle(
            frameWidth * 0, frameHeight * 0,
            frameHeight, frameHeight
        );
        
        Destination = new Rectangle(
            location.X, location.Y,
            frameHeight, frameHeight
        );
        
        Color = Color.White;

        _origin = location;
        ActualPosition = location.ToVector2();

        CollisionBox = new BoxCollider(
            Point.Zero,
            Point.Zero,
            offsetRatio: new Vector2(0.5f, 1f),
            solid: false,
            owner: this
        );
        
        MeleeRange = new Rectangle(
            CollisionBox.Bounds.Right, CollisionBox.Bounds.Top,
            CollisionBox.Bounds.Width, CollisionBox.Bounds.Height
        );

        AnimationManager = new AnimationManager(AnimationLibrary.BatumbakalAnimations);
        AnimationManager.Play("Idle");
    }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) { 
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        _input.Update();

        #region --- DEBUGGING ---
        if (_input.Reset) {
            // Velocity = Vector2.Zero;
            ActualPosition = _origin.ToVector2();
        }
        
        if (_input.Teleport) {
            Velocity = Vector2.Zero;
            ActualPosition = _input.TeleportLocation.ToVector2();
        }
        
        // Console.WriteLine(Destination.Location);
        #endregion
        
        #region --- MOVEMENT ---
        if (CanMove) {
            Direction = Vector2.Zero;
            if (_input.MoveLeft) {
                Direction.X -= 1;
                IsFacingRight = false;
            } else if (_input.MoveRight) {
                Direction.X += 1;
                IsFacingRight = true;
            }
        }
        #endregion
        
        #region --- JUMP ---
        if (CanJump) {
            if (_input.PressJump && JumpCount > 0 && !_isHoldingJump) {
                Velocity.Y = -JumpPower;
                _isHoldingJump = true;
                JumpCount--;
                Direction.Y = -1;
            }

            if (_input.ReleaseJump) {
                _isHoldingJump = false;

                if (IsOnGround && !IsJumping) {
                    JumpCount = 2;
                }
            }
        }
        #endregion

        #region --- ATTACK ---
        if (_input.PressAttack && !IsAttacking && AttackCooldownTimer <= 0f) {
            IsAttacking = true;
            AnimationManager.Play("Attack");
            AttackCooldownTimer = AttackCooldown;
        }
        
        if (IsFacingRight) {
            MeleeRange.Location = new Point(
                CollisionBox.Bounds.Right,
                CollisionBox.Bounds.Top
            );
        } else {
            MeleeRange.Location = new Point(
                CollisionBox.Bounds.Left - MeleeRange.Width,
                CollisionBox.Bounds.Top
            );
        }
        
        if (IsAttacking && CurrentAnimation.IsFinished) {

            MeleeRange.Size = new Point(CollisionBox.Bounds.Width, CollisionBox.Bounds.Height);

            CheckMeleeRange(stage);
            
            IsAttacking = false;
            // MeleeRange.Location = Point.Zero;
            // MeleeRange.Size = Point.Zero;
        }

        if (!IsAttacking && AttackCooldownTimer > 0f) {
            AttackCooldownTimer -= deltaTime;
            if (AttackCooldownTimer < 0f) AttackCooldownTimer = 0f;
        }
        #endregion
        
        #region --- GET HIT ---
        if (Health.WasDecreased) {
            IsHit = true;
            AnimationManager.Play("Hit");
            InvincibilityTimer = InvincibilityFrames;
            Health.WasDecreased = false;
        }

        if (IsHit && CurrentAnimation.IsFinished) {
            IsHit = false;
        }

        if (!IsHit && InvincibilityTimer > 0f) {
            InvincibilityTimer -= deltaTime;
            if (InvincibilityTimer < 0f) InvincibilityTimer = 0f;
        }
        #endregion
        
        CheckState();

        #region --- VELOCITY ---
        Velocity.X = Direction.X * WalkSpeed;
        Velocity += gravity.Force * deltaTime;
        if (IsAttacking) {
            Velocity *= 0.2f;
        }
        #endregion

        #region --- COLLISION ---
        NextPosition.X = ActualPosition.X + Velocity.X * deltaTime;
        NextPosition.Y = ActualPosition.Y + Velocity.Y * deltaTime;
        CollisionBox.GetNextBounds(ActualPosition, NextPosition);
        CheckCollision(stage.GetCollisionManager);
        #endregion
        
        #region --- POSITION ---
        Destination.Location = ActualPosition.ToPoint();
        // UpdateGamePos();
        #endregion
        
        AnimationManager.Update();
        
        #region --- SAVE DATA ---
        if (_input.KeybindSave) {
            SaveData saveData = new SaveData {
                CurrentScene = stage.GetName(),
                PositionX = ActualPosition.X,
                PositionY = ActualPosition.Y,
                CurrentHealth = Health.Current,
                MaximumHealth = Health.Maximum
            };

            SaveManager.Save(saveData);
        }
        #endregion
    }

    public override void Draw(SpriteBatch spriteBatch) {
        SpriteEffects flip = IsFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        // spriteBatch.Draw(MyDebug.Texture, MeleeRange, Color.Blue * 0.5f);
        
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
        
        DrawAttributes(spriteBatch);
        
        string gemsText = $"GEMS : {_gems}";
        
        spriteBatch.DrawString(FontLibrary.TempFont, gemsText, new Vector2(25, 50), Color.White);
    }
    
    protected override void CheckState() {
        IsInvincible = InvincibilityTimer > 0f;
        IsJumping = Velocity.Y < 0 && !IsOnGround;
        IsFalling = Velocity.Y > 0 && !IsOnGround;
        IsWalking = Direction.X != 0 && IsOnGround;
        IsIdle = IsOnGround && !IsWalking && !IsJumping && !IsFalling && !IsAttacking;
        IsAlive = Health.Current > 0;

        if (!IsAlive) {
            Velocity.X = 0;
            Direction = Vector2.Zero;
            CanMove = false;
            CanJump = false;
            AnimationManager.Play("Death");

            if (!IsDead && AnimationManager.CurrentAnimation.IsFinished) {
                IsDead = true;
            }
        }
        
        if (IsHit) AnimationManager.Play("Hit");
        else if (IsAttacking) AnimationManager.Play("Attack");
        else if (IsJumping) AnimationManager.Play("Jump");
        else if (IsFalling) AnimationManager.Play("Fall");
        else if (IsWalking) AnimationManager.Play("Walk");
        else if (IsIdle) AnimationManager.Play("Idle");
    }
}

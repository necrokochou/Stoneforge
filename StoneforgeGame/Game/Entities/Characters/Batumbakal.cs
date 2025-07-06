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
    
    private bool _isHoldingJump;

    private Point _origin;


    // CONSTRUCTORS
    public Batumbakal() {
        Texture = TextureLibrary.Batumbakal;
        
        Name = "Batumbakal";
        Health = new Health(30);
        
        WalkSpeed = 200f;
        JumpPower = 550f;
        JumpCount = 2;
        InvincibilityFrames = 3f;
        AttackCooldown = 1f;
        
        IsFacingRight = true;
        
        CanJump = true;
        CanMove = true;
    }


    // PROPERTIES
    private Animation CurrentAnimation {
        get => AnimationManager.CurrentAnimation;
    }


    // METHODS
    public override void Load(Rectangle window, Point location, int sizeMultiplier = 1) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        Source = new Rectangle(
            frameWidth * 0, frameHeight * 0,
            frameWidth, frameHeight
        );
        Destination = new Rectangle(
            location.X, location.Y,
            frameWidth * sizeMultiplier, frameHeight * sizeMultiplier
        );
        Color = Color.White;

        CollisionBox = new BoxCollider(
            Destination.Location,
            Destination.Location + Destination.Size,
            solid : false, owner : this
        );

        MeleeRange = new Rectangle(
            CollisionBox.Bounds.Right - CollisionBox.Bounds.Width / 2, CollisionBox.Bounds.Top,
            CollisionBox.Bounds.Width / 2, CollisionBox.Bounds.Height
        );

        _origin = location;
        if (_origin.X < 0 || _origin.X >= window.Right - Source.Width) _origin.X = 0;
        if (_origin.Y < 0 || _origin.Y >= window.Bottom - Source.Height) _origin.Y = 0;
        ActualPosition = location.ToVector2();

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
        
        if (IsAttacking && CurrentAnimation.IsFinished) {
            if (IsFacingRight) {
                MeleeRange.Location = new Point(
                    CollisionBox.Bounds.Right - CollisionBox.Bounds.Width / 2,
                    CollisionBox.Bounds.Top
                );
            } else {
                MeleeRange.Location = new Point(
                    CollisionBox.Bounds.Left - MeleeRange.Width,
                    CollisionBox.Bounds.Top
                );
            }

            MeleeRange.Size = new Point(CollisionBox.Bounds.Width / 2, CollisionBox.Bounds.Height);

            CheckMeleeRange(stage);
            
            IsAttacking = false;
            MeleeRange.Location = Point.Zero;
            MeleeRange.Size = Point.Zero;
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
        // Velocity = Vector2.Clamp(Velocity, Vector2.Zero, new Vector2(WalkSpeed, gravity.Magnitude));
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
        UpdateGamePos();
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
    }
}

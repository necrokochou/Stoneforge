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
        
        IsFacingRight = true;
    }


    // PROPERTIES



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

        if (_input.Reset) {
            ActualPosition = _origin.ToVector2();
        }
        
        Direction = Vector2.Zero;

        if (_input.MoveLeft) {
            Direction.X -= 1;
            IsFacingRight = false;
        } else if (_input.MoveRight) {
            Direction.X += 1;
            IsFacingRight = true;
        }
        
        if (_input.PressJump && JumpCount > 0 && !_isHoldingJump) {
            Velocity.Y = -JumpPower;
            _isHoldingJump = true;
            JumpCount--;
        }

        if (_input.ReleaseJump) {
            _isHoldingJump = false;
        }
        
        if (_input.Teleport) {
            ActualPosition = _input.TeleportLocation.ToVector2();
        }
        
        CheckState();
        
        Velocity.X = Direction.X * WalkSpeed;
        Velocity.Y += gravity.Magnitude * deltaTime;
        
        if (Math.Abs(Velocity.Y) < 0.01f) {
            Velocity.Y = 0f;
        }

        NextPosition.X = ActualPosition.X + Velocity.X * deltaTime;
        CollisionBox.GetNextBoundsX(ActualPosition, Velocity.X * deltaTime);
        if (!CollisionBox.HasCollided(collisionManager, CollisionBox.NextHorizontalBounds)) {
            ActualPosition.X = NextPosition.X;
        } else {
            Velocity.X = 0;
        }
        
        NextPosition.Y = ActualPosition.Y + Velocity.Y * deltaTime;
        CollisionBox.GetNextBoundsY(ActualPosition, Velocity.Y * deltaTime);
        if (!CollisionBox.HasCollided(collisionManager, CollisionBox.NextVerticalBounds)) {
            ActualPosition.Y = NextPosition.Y;
            IsOnGround = false;
        } else {
            Velocity.Y = 0;
            IsOnGround = true;
            IsJumping = false;
            JumpCount = 2;
        }
        
        Destination.Location = ActualPosition.ToPoint();
        CollisionBox.Update(Destination);
        AnimationManager.Update();
        
        Console.WriteLine(Velocity);
        // Console.WriteLine($"{Velocity} {deltaTime} {ActualPosition.X} {ActualPosition.Y}");
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

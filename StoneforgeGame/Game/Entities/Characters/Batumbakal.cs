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


namespace StoneforgeGame.Game.Entities.Characters;


public class Batumbakal : Character {
    // FIELDS
    private InputManager _input = new InputManager();

    private bool _isOnGround;
    private bool _isJumping;

    private Point _origin;


    // CONSTRUCTORS
    public Batumbakal() {
        Name = "Batumbakal";
        Texture = TextureLibrary.Batumbakal;
        WalkSpeed = 200f;
        JumpPower = 700f;
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
        
        Animations.Add(
            "Idle",
            new Animation(Texture, new Point(0, 0), new Point(0, 11), 1)
        );
        
        CurrentAnimation = Animations["Idle"];
    }

    public override void Update(GameTime gameTime, CollisionManager collisionManager, Gravity gravity) { 
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        _input.Update();

        if (_input.Reset) {
            ActualPosition = _origin.ToVector2();
        }
        
        Direction = Point.Zero;

        if (_input.MoveLeft) {
            Direction.X = -1;
        } else if (_input.MoveRight) {
            Direction.X = 1;
        }
        
        if (_input.MoveJump && !_isJumping) {
            Velocity.Y = -JumpPower;
        }
        
        if (_input.Teleport) {
            ActualPosition = _input.TeleportLocation.ToVector2();
        }
        
        Velocity.X = Direction.X * WalkSpeed;
        
        CollisionBox.GetNextBoundsX(ActualPosition, Velocity.X, deltaTime);
        if (!CollisionBox.HasCollided(collisionManager, CollisionBox.NextHorizontalBounds)) {
            ActualPosition.X = CollisionBox.NextHorizontalBounds.X;
        } else {
            Velocity.X = 0;
        }
        
        Velocity.Y += gravity.Magnitude * deltaTime;
        
        CollisionBox.GetNextBoundsY(ActualPosition, Velocity.Y, deltaTime);
        if (!CollisionBox.HasCollided(collisionManager, CollisionBox.NextVerticalBounds)) {
            ActualPosition.Y = CollisionBox.NextVerticalBounds.Y;
        } else {
            if (Velocity.Y > 0) {
                _isJumping = false;
            }
            
            Velocity.Y = 0;
        }
        
        Destination.Location = ActualPosition.ToPoint();
        CollisionBox.Update(Destination);
        CurrentAnimation.Update();
    }

    public override void Draw(SpriteBatch spriteBatch) {
        CollisionBox.Draw(spriteBatch, Color.Yellow * 0.5f, 2);
        spriteBatch.Draw(Texture.Image, Destination, Source, Color);
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Attributes;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;


namespace StoneforgeGame.Game.Entities.Characters;


public class Skeleton : Enemy {
    // FIELDS
    


    // CONSTRUCTORS
    public Skeleton() {
        Texture = TextureLibrary.SkeletonIdle;
        
        Name = "Skeleton";
        Health = new Health(30);

        WalkSpeed = 100f;
        AttackCooldown = 3f;
        
        IsFacingRight = true;
    }


    // PROPERTIES



    // METHODS
    public override void Load(Rectangle window, Point location) {
        CalculateSource();
        CalculateDestination(location);
        Color = Color.White;
        
        float widthRatio = 0.7f;
        float heightRatio = 0.5f;
        
        int colliderWidth = (int)(Destination.Width * widthRatio);
        int colliderHeight = (int)(Destination.Height * heightRatio);

        int colliderX = Destination.X + (Destination.Width - colliderWidth) / 2;
        int colliderY = Destination.Bottom - colliderHeight;

        CollisionBox = new BoxCollider(
            new Point(colliderX, colliderY),
            new Point(colliderX + colliderWidth, colliderY + colliderHeight),
            offsetRatio: new Vector2(widthRatio, heightRatio),
            solid: false,
            owner: this
        );
        
        MeleeRange = new Rectangle(
            CollisionBox.Bounds.Right - CollisionBox.Bounds.Width / 2, CollisionBox.Bounds.Top,
            CollisionBox.Bounds.Width / 2, CollisionBox.Bounds.Height
        );
        
        ActualPosition = location.ToVector2();

        AnimationManager = new AnimationManager(AnimationLibrary.SkeletonAnimations);
        AnimationManager.Play("Idle");

        PatrolPoints = [new Vector2(528, Destination.Y), new Vector2(816, Destination.Y)];
    }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) {
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        Direction = Vector2.Zero;

        if (!IsPatrolling) {
            PatrolPointIndex = (PatrolPointIndex + 1) % PatrolPoints.Count;
        }

        Patrol(deltaTime);
        CheckState();
        
        AnimationManager.Update();
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
    }

    protected override void CheckState() {
        IsIdle = !IsPatrolling && !IsAttacking;
        
        if (IsPatrolling) {
            Texture = TextureLibrary.SkeletonWalk;
            CalculateSource();
            CalculateDestination(Destination.Location);
            CalculateCollisionBox();
            AnimationManager.Play("Walk");
        }
        else if (IsIdle) {
            Texture = TextureLibrary.SkeletonIdle;
            CalculateSource();
            CalculateDestination(Destination.Location);
            CalculateCollisionBox();
            AnimationManager.Play("Idle");
        }
    }

    private void CalculateSource() {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        Source = new Rectangle(
            frameWidth * 0, frameHeight * 0,
            frameWidth, frameHeight
        );
    }

    private void CalculateDestination(Point location) {
        int frameWidth = Texture.Image.Width / Texture.Columns;
        int frameHeight = Texture.Image.Height / Texture.Rows;
        
        Destination = new Rectangle(
            location.X,
            location.Y,
            frameWidth,
            frameHeight
        );
    }
    
    private void CalculateCollisionBox() {
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Attributes;
using StoneforgeGame.Game.Graphics;
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
        AttackDamage = 10f;
        AttackCooldown = 3f;
        
        IsFacingRight = true;
    }


    // PROPERTIES



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

        ActualPosition = location.ToVector2();

        CollisionBox = new BoxCollider(
            Point.Zero,
            Point.Zero,
            offsetRatio: new Vector2(0.5f, 0.4f),
            solid: false,
            owner: this
        );
        
        MeleeRange = new Rectangle(
            CollisionBox.Bounds.Right, CollisionBox.Bounds.Top,
            CollisionBox.Bounds.Width, CollisionBox.Bounds.Height
        );

        AnimationManager = new AnimationManager(AnimationLibrary.SkeletonAnimations);
        AnimationManager.Play("Idle");

        PatrolPoints = [new Vector2(528, Destination.Y), new Vector2(816, Destination.Y)];
    }

    private void Unload() {
    }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) {
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        Direction = Vector2.Zero;

        Patrol(deltaTime, gravity, stage);
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
        IsAlive = Health.Current > 0;

        if (!IsAlive && !IsDead) {
            IsDead = true;
            Direction = Vector2.Zero;
            CanMove = false;
        }
        
        if (IsPatrolling) {
            Texture = TextureLibrary.SkeletonWalk;
            AnimationManager.Play("Walk");
        }
        else if (IsIdle) {
            Texture = TextureLibrary.SkeletonIdle;
            AnimationManager.Play("Idle");
        }
    }
}

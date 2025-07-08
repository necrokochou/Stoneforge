using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Attributes;
using StoneforgeGame.Game.Graphics;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Entities.Characters;


public class Skeleton : Enemy {
    // FIELDS
    private bool _isFollowingPlayer;
    private bool _isInAttackRange;
    private bool _isAttackAnimationCompleted;
    private float _attackCooldownTimer;
    


    // CONSTRUCTORS
    public Skeleton() {
        Texture = TextureLibrary.SkeletonIdle;
        
        Name = "Skeleton";
        Health = new Health(30);

        WalkSpeed = 100f;
        AttackDamage = 10f;
        AttackCooldown = 3f;
        DetectRange = 200f;
        
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
            offsetRatio: new Vector2(0.2f, 0.4f),
            solid: false,
            owner: this
        );

        AnimationManager = new AnimationManager(AnimationLibrary.SkeletonAnimations);
        AnimationManager.Play("Idle");
    }

    private void Unload() {
    }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) {
        float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
        
        Direction = Vector2.Zero;

        MeleeRange.Size = new Point((int) (CollisionBox.Bounds.Width * 1.5f), CollisionBox.Bounds.Height);
        if (IsFacingRight) {
            MeleeRange.Location = new Point(CollisionBox.Bounds.Right, CollisionBox.Bounds.Top);
        } else {
            MeleeRange.Location = new Point(CollisionBox.Bounds.Left - MeleeRange.Width, CollisionBox.Bounds.Top);
        }

        if (IsAttacking || _isFollowingPlayer)
        {
            IsPatrolling = false;
            FollowPlayer(stage.GetPlayer(), deltaTime, gravity, stage);
        }
        else
        {
            Patrol(deltaTime, gravity, stage);
        }

        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= deltaTime;

        CheckState();
        
        AnimationManager.Update();
    }

    public override void Draw(SpriteBatch spriteBatch) {
        SpriteEffects flip = IsFacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        spriteBatch.Draw(MyDebug.Texture, MeleeRange, Color.Blue * 0.5f);
        
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
        IsIdle = !IsPatrolling && !IsAttacking && !_isFollowingPlayer;
        IsAlive = Health.Current > 0;

        if (!IsAlive && !IsDead) {
            IsDead = true;
            Direction = Vector2.Zero;
            CanMove = false;
        }

        if (IsAttacking) {
            Texture = TextureLibrary.SkeletonAttack;
            if (!AnimationManager.IsPlaying("Attack")) {
                AnimationManager.Play("Attack");
                _isAttackAnimationCompleted = false;
            }

            if (AnimationManager.CurrentAnimation.IsFinished) {
                _isAttackAnimationCompleted = true;
                IsAttacking = false;
            }
        } else if (IsPatrolling || _isFollowingPlayer) {
            Texture = TextureLibrary.SkeletonWalk;
            AnimationManager.Play("Walk");
        } else if (IsIdle) {
            Texture = TextureLibrary.SkeletonIdle;
            AnimationManager.Play("Idle");
        }
    }

    private void Patrol(float deltaTime, Gravity gravity, Stage stage) {
        var player = stage.GetPlayer();
        float distanceToPlayer = Vector2.Distance(ActualPosition, player.ActualPosition);

        if (distanceToPlayer <= DetectRange) {
            _isFollowingPlayer = true;
            return;
        }
        
        IsPatrolling = true;
    
        int target = PatrolPoints[PatrolPointIndex];
        float distance = target - ActualPosition.X;

        if (Math.Abs(distance) < 5f) {
            ActualPosition.X = target;
            PatrolPointIndex = (PatrolPointIndex + 1) % PatrolPoints.Count;
            return;
        }

        if (CanMove) {
            if (distance < 0) {
                Direction.X -= 1;
                IsFacingRight = false;
            } else {
                Direction.X += 1;
                IsFacingRight = true;
            }
        }

        Velocity.X = (IsAttacking && !_isAttackAnimationCompleted) ? 0 : Direction.X * WalkSpeed;
        Velocity += gravity.Force * deltaTime;

        NextPosition.X = ActualPosition.X + Velocity.X * deltaTime;
        NextPosition.Y = ActualPosition.Y + Velocity.Y * deltaTime;
        CollisionBox.GetNextBounds(ActualPosition, NextPosition);
        CheckCollision(stage.GetCollisionManager());

        Destination.Location = ActualPosition.ToPoint();
    }
    
    private void FollowPlayer(Character player, float deltaTime, Gravity gravity, Stage stage) {
        float distanceToPlayer = Vector2.Distance(ActualPosition, player.ActualPosition);
        _isFollowingPlayer = distanceToPlayer <= DetectRange;
        
        _isInAttackRange = MeleeRange.Intersects(player.GetCollisionBox().Bounds);

        if (_isFollowingPlayer) {
            IsPatrolling = false;

            if (_isInAttackRange) {
                // Face the player ONCE when attack starts
                if (!IsAttacking) {
                    IsFacingRight = player.ActualPosition.X > ActualPosition.X;
                    Direction = Vector2.Zero;
                    Velocity = Vector2.Zero;

                    if (_attackCooldownTimer <= 0) {
                        IsAttacking = true;
                        _attackCooldownTimer = AttackCooldown;
                        player.GetHealth().Decrease(AttackDamage);
                    }
                }

                // No movement while attacking
                Destination = new Rectangle((int)ActualPosition.X, (int)ActualPosition.Y, Destination.Width, Destination.Height);
                CollisionBox.Update();
                return;
            }

            // Move toward the player ONLY if not in attack range
            Direction.X = player.ActualPosition.X < ActualPosition.X ? -1 : 1;
            IsFacingRight = Direction.X > 0;

            if (CanMove) {
                Velocity.X = Direction.X * WalkSpeed;
                Velocity += gravity.Force * deltaTime;

                Vector2 nextPosition = ActualPosition + Velocity * deltaTime;
                CollisionBox.GetNextBounds(ActualPosition, nextPosition);

                if (!CollisionBox.HasCollided(stage.GetCollisionManager(), CollisionBox.NextHorizontalBounds)) {
                    ActualPosition.X = nextPosition.X;
                }

                CollisionBox.GetNextBounds(ActualPosition, new Vector2(ActualPosition.X, ActualPosition.Y + Velocity.Y));
                if (!CollisionBox.HasCollided(stage.GetCollisionManager(), CollisionBox.NextVerticalBounds)) {
                    ActualPosition.Y += Velocity.Y;
                } else {
                    Velocity.Y = 0;
                }
            }
        }

        Destination = new Rectangle((int)ActualPosition.X, (int)ActualPosition.Y, Destination.Width, Destination.Height);
        CollisionBox.Update();
    }
}

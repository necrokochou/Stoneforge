using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;


namespace StoneforgeGame.Game.Entities.Characters;


public abstract class Enemy : Character {
    // FIELDS
    protected List<Vector2> PatrolPoints;
    
    protected int PatrolPointIndex;
    protected bool IsPatrolling;
    protected bool PatrolPointReached;


    // CONSTRUCTORS
    


    // PROPERTIES



    // METHODS
    public override void Load(Point location) { }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) { }

    public override void Draw(SpriteBatch spriteBatch) { }

    protected virtual void Patrol(float deltaTime, Gravity gravity, Stage stage) {
        IsPatrolling = true;
    
        Vector2 target = PatrolPoints[PatrolPointIndex];
        float distance = target.X - ActualPosition.X;

        if (Math.Abs(distance) < 5f) {
            ActualPosition.X = target.X;
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

        Velocity.X = IsAttacking ? 0 : Direction.X * WalkSpeed;
        Velocity += gravity.Force * deltaTime;

        NextPosition.X = ActualPosition.X + Velocity.X * deltaTime;
        NextPosition.Y = ActualPosition.Y + Velocity.Y * deltaTime;
        CollisionBox.GetNextBounds(ActualPosition, NextPosition);
        CheckCollision(stage.GetCollisionManager);

        Destination.Location = ActualPosition.ToPoint();
    }
}

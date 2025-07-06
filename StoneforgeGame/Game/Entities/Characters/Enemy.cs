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
    public override void Load(Rectangle window, Point location) { }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) { }

    public override void Draw(SpriteBatch spriteBatch) { }

    protected virtual void Patrol(float deltaTime) {
        IsPatrolling = true;
        
        Vector2 target = PatrolPoints[PatrolPointIndex];
        float distance = target.X - ActualPosition.X;
        
        if (distance > -5f && distance < 5f) {
            ActualPosition.X = target.X;
            PatrolPointIndex = (PatrolPointIndex + 1);
            if (PatrolPointIndex >= PatrolPoints.Count) {
                PatrolPointIndex = 0;
            }
            return;
        }
        
        if (distance < 0) {
            Direction.X = -1f;
            IsFacingRight = false;
        } else if (distance > 0) {
            Direction.X = 1f;
            IsFacingRight = true;
        } else {
            Direction.X = 0f;
        }
        
        Velocity.X = IsAttacking ? 0 : Direction.X * WalkSpeed;
        ActualPosition.X += Velocity.X * deltaTime;
        
        Destination.Location = ActualPosition.ToPoint();
    }
}

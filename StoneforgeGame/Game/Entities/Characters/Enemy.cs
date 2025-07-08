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
    public List<int> PatrolPoints;
    
    protected int PatrolPointIndex;
    protected bool IsPatrolling;
    protected bool PatrolPointReached;
    protected float DetectRange;


    // CONSTRUCTORS
    


    // PROPERTIES



    // METHODS
    public override void Load(Point location) { }

    public override void Update(GameTime gameTime, Stage stage, Gravity gravity) { }

    public override void Draw(SpriteBatch spriteBatch) { }
}

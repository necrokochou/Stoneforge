using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Entities.ObjectTiles;


public abstract class ObjectTile {
    // FIELDS
    protected Texture Texture;
    protected Rectangle Destination;
    protected Rectangle Source;
    protected Color Color;
    
    protected BoxCollider CollisionBox;

    protected Vector2 ActualPosition;
    
    protected AnimationManager AnimationManager;

    public bool IsDestroyable;
    public bool IsDestroyed;


    // CONSTRUCTORS
    


    // PROPERTIES

    

    // METHODS
    public abstract void Load(Point location, Point size = default);
    
    public abstract void Update();
    
    public abstract void Draw(SpriteBatch spriteBatch);

    public virtual BoxCollider GetCollisionBox() {
        return CollisionBox;
    }
    
    public virtual void OnDestroy() { }

    protected virtual void Destroy() {
        IsDestroyed = true;
    }
}

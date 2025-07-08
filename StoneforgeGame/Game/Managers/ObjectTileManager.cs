using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Managers;


public class ObjectTileManager {
    // FIELDS
    private List<ObjectTile> _objectTiles = new List<ObjectTile>();
    private List<ObjectTile> _destroyedObjectTiles = new List<ObjectTile>();
    
    private bool _debug = false;


    // CONSTRUCTORS
    
    

    // PROPERTIES
    public List<ObjectTile> ObjectTiles {
        get => _objectTiles;
    }
    public List<ObjectTile> DestroyedObjectTiles {
        get => _destroyedObjectTiles;
    }


    // METHODS
    public void Load() {

    }

    public void Unload() {
        _objectTiles.Clear();
    }

    public void Update() {
        for (int i = _objectTiles.Count - 1; i >= 0; i--) {
            var tile = _objectTiles[i];
            tile?.Update();
            if (MyDebug.IsDebug) tile?.DebugDragTile();
            if (tile?.IsDestroyed == true) {
                _objectTiles.RemoveAt(i);
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach (ObjectTile objectTile in _objectTiles) {
            if (_debug) objectTile?.GetCollisionBox().Draw(spriteBatch, 2);
            objectTile?.Draw(spriteBatch);
        }
    }

    public void Add(ObjectTile objectTile) {
        _objectTiles.Add(objectTile);
    }

    public void Add(ObjectTile objectTile, Point location) {
        objectTile.Load(location);
        
        Add(objectTile);
    }

    public void Remove(ObjectTile objectTile) {
        _destroyedObjectTiles.Add(objectTile);
        _objectTiles.Remove(objectTile);
    }
}

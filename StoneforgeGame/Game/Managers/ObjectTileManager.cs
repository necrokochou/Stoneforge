using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.ObjectTiles;


namespace StoneforgeGame.Game.Managers;


public class ObjectTileManager {
    // FIELDS
    private List<ObjectTile> _objectTiles;
    
    private bool _debug = false;


    // CONSTRUCTORS
    public ObjectTileManager() {
        _objectTiles = new List<ObjectTile>();
    }


    // PROPERTIES
    public List<ObjectTile> ObjectTiles {
        get => _objectTiles;
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
            tile?.DebugDragTile();
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

    private void Add(ObjectTile objectTile) {
        _objectTiles.Add(objectTile);
    }

    public void Add(ObjectTile objectTile, Point location) {
        objectTile.Load(location);
        
        Add(objectTile);
    }

    public void Remove(ObjectTile objectTile) {
        _objectTiles.Remove(objectTile);
    }
}

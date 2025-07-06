using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.ObjectTiles;


namespace StoneforgeGame.Game.Managers;


public class ObjectTileManager {
    // FIELDS
    private List<ObjectTile> _objectTiles;


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
        foreach (ObjectTile objectTile in _objectTiles) {
            objectTile?.Update();

            if (objectTile?.IsDestroyed == true) {
                Remove(objectTile);
                return;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach (ObjectTile objectTile in _objectTiles) {
            objectTile?.Draw(spriteBatch);
        }
    }

    private void Add(ObjectTile objectTile) {
        _objectTiles.Add(objectTile);
    }

    public void Remove(ObjectTile objectTile) {
        _objectTiles.Remove(objectTile);
    }

    public void Add(ObjectTile objectTile, Point location, Point size = default) {
        objectTile.Load(location, size);
        
        Add(objectTile);
    }

}

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;


namespace StoneForgeGame.Game.Managers;


public class CharacterManager {
    // FIELDS
    private List<Character> _characters = new List<Character>();
    private CollisionManager _collisionManager;


    // CONSTRUCTORS
    public CharacterManager(CollisionManager collisionManager) {
        _collisionManager = collisionManager;
    }


    // PROPERTIES
    


    // METHODS
    public void Load() {
        
    }

    public void Unload() {
        _characters.Clear();
    }
    
    public void Update(GameTime gameTime, Gravity gravity) {
        foreach (Character character in _characters) {
            character.Update(gameTime, _collisionManager, gravity);
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach (Character character in _characters) {
            character.Draw(spriteBatch);
        }
    }

    public void Add(Character character) {
        _characters.Add(character);
    }
    
    public void Remove(Character character) {
        _characters.Remove(character);
    }
}

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Stages;


namespace StoneForgeGame.Game.Managers;


public class CharacterManager {
    // FIELDS
    private List<Character> _characters = new List<Character>();
    
    private Stage _stage;

    private bool _debug = true;


    // CONSTRUCTORS
    


    // PROPERTIES
    public List<Character> Characters {
        get => _characters;
    }


    // METHODS
    public void Load(Stage stage) {
        _stage = stage;
    }

    public void Unload() {
        _characters.Clear();
    }
    
    public void Update(GameTime gameTime, Gravity gravity) {
        foreach (Character character in _characters) {
            character.Update(gameTime, _stage, gravity);
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        foreach (Character character in _characters) {
            if (_debug) character.GetCollisionBox().Draw(spriteBatch, 2);
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

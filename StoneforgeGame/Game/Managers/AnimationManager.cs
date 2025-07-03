using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Graphics;


namespace StoneforgeGame.Game.Managers;


public class AnimationManager {
    // FIELDS
    private Dictionary<string,Animation> _animations;


    // CONSTRUCTORS



    // PROPERTIES



    // METHODS
    public void Update(GameTime gameTime) {
        
    }
    
    public void Add(string name, Animation animation) {
        _animations.Add(name, animation);
    }
    
    public void Remove(string name) {
        _animations.Remove(name);
    }
}

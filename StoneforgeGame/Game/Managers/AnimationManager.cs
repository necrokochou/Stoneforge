using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Graphics;


namespace StoneforgeGame.Game.Managers;


public class AnimationManager {
    // FIELDS
    private Dictionary<string,Animation> _animations;
    private Animation _currentAnimation;


    // CONSTRUCTORS
    public AnimationManager(Dictionary<string, Animation> animations) {
        _animations = animations;
    }


    // PROPERTIES
    


    // METHODS
    public void Update() {
        _currentAnimation?.Update();
    }

    public void Play(string name) {
        if (_animations.ContainsKey(name)) {
            _currentAnimation = _animations[name];
        }
    }

    public Rectangle GetFrame(int frameWidth, int frameHeight) {
        return _currentAnimation.GetFrame(frameWidth, frameHeight);
    }
}

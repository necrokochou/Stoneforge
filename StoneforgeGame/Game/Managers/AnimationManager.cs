using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Graphics;


namespace StoneforgeGame.Game.Managers;


public class AnimationManager {
    // FIELDS
    private Dictionary<string, Animation> _animations;
    private Animation _currentAnimation;


    // CONSTRUCTORS
    public AnimationManager(Dictionary<string, Animation> animations) {
        _animations = animations;
    }


    // PROPERTIES
    public Dictionary<string, Animation> Animations {
        get => _animations;
    }
    public Animation CurrentAnimation {
        get => _currentAnimation;
    }


    // METHODS
    public void Update() {
        _currentAnimation?.Update();
    }

    public void Play(string name) {
        if (!_animations.ContainsKey(name))
            return;

        if (_currentAnimation == _animations[name])
            return;

        _currentAnimation = _animations[name];
        _currentAnimation.Reset();
    }

    public Rectangle GetFrame(int frameWidth, int frameHeight) {
        return _currentAnimation.GetFrame(frameWidth, frameHeight);
    }
}

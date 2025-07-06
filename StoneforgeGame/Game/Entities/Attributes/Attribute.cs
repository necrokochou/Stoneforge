using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Libraries;


namespace StoneforgeGame.Game.Entities.Attributes;


public abstract class Attribute {
    // FIELDS
    private string _name;
    private float _current;
    private float _maximum;

    private bool _wasDecreased;


    // CONSTRUCTORS
    protected Attribute(string name, float maximum) {
        _name = name;
        _current = maximum;
        _maximum = maximum;
    }


    // PROPERTIES
    public float Current {
        get => _current;
        set => _current = value;
    }
    public float Maximum {
        get => _maximum;
        set => _maximum = value;   
    }
    public bool WasDecreased {
        get => _wasDecreased;
        set => _wasDecreased = value;
    }


    // METHODS
    public void Draw(SpriteBatch spriteBatch) {
        string text = $"{_name} : {_current}/{_maximum}";
        
        spriteBatch.DrawString(FontLibrary.TempFont, text, new Vector2(25, 25), Color.White);
    }
    
    public virtual void Increase(float amount) {
        if (_current + amount < _maximum)
            _current += amount;
    }

    public virtual void Decrease(float amount) {
        if (_current > 0) _current -= amount;
        if (_current <= 0) _current = 0;
        
        _wasDecreased = true;
    }

    protected virtual void Reset() {
        _current = _maximum;
    }
}

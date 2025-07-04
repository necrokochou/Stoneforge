using System;
using Microsoft.Xna.Framework;
using Texture = StoneforgeGame.Game.Graphics.Texture;


namespace StoneforgeGame.Game.Graphics;


public class Animation {
    // FIELDS
    private Texture _texture;
    private int _counter;
    private int _interval;
    private int _rowIndex;
    private int _columnIndex;
    private Point _startFrame;
    private Point _endFrame;
    private bool _playOnce;
    private bool _endOnLastFrame;
    private bool _isFinished;


    // CONSTRUCTORS
    public Animation(Texture texture, Point startFrame, Point endFrame, int interval, bool playOnce, bool endOnLastFrame = false) {
        _texture = texture;
        _startFrame = startFrame;
        _endFrame = endFrame;
        _interval = interval;
        _playOnce = playOnce;
        _endOnLastFrame = playOnce && endOnLastFrame;
        
        _rowIndex = _startFrame.X;
        _columnIndex = _startFrame.Y;
    }


    // PROPERTIES
    public bool IsFinished {
        get => _isFinished;
    }
    

    // METHODS
    public void Update() {
        _counter++;

        if (_counter >= _interval) {
            _counter = 0;
            
            NextFrame();
        }
    }

    private void NextFrame() {
        _columnIndex++;
        
        if (_columnIndex >= _texture.Columns) {
            _columnIndex = 0;
            _rowIndex++;
        }

        bool reachedEndFrame = 
            (_rowIndex > _endFrame.X) || 
            (_rowIndex == _endFrame.X && _columnIndex > _endFrame.Y);

        if (_playOnce && reachedEndFrame) {
            _isFinished = true;

            if (_endOnLastFrame) {
                _rowIndex = _endFrame.X;
                _columnIndex = _endFrame.Y;
            }

            return;
        }

        if (!_playOnce && reachedEndFrame) Reset();
    }

    public void Reset() {
        _rowIndex = _startFrame.X;
        _columnIndex = _startFrame.Y;
        _counter = 0;
        
        _isFinished = false;
    }

    public Rectangle GetFrame(int frameWidth, int frameHeight) {
        return new Rectangle(frameWidth * _columnIndex, frameHeight * _rowIndex, frameWidth, frameHeight);
    }
}

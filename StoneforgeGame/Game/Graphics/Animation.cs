﻿using System;
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
    private bool _endOnLastFrame;
    private int _skipFrame;
    private bool _isFinished;


    // CONSTRUCTORS
    public Animation(Texture texture, Point startFrame, Point endFrame, int interval, bool endOnLastFrame = false, int skipFrame = 0) {
        _texture = texture;
        _startFrame = startFrame;
        _endFrame = endFrame;
        _interval = interval;
        _endOnLastFrame = endOnLastFrame;
        _skipFrame = skipFrame;
        
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
        _columnIndex += _skipFrame;
        
        if (_columnIndex >= _texture.Columns) {
            _columnIndex = 0;
            _rowIndex++;
        }

        bool reachedEndFrame = 
            (_rowIndex > _endFrame.X) || 
            (_rowIndex == _endFrame.X && _columnIndex > _endFrame.Y);

        if (_endOnLastFrame && reachedEndFrame) {
            _isFinished = true;
            _rowIndex = _endFrame.X;
            _columnIndex = _endFrame.Y;

            return;
        }

        if (!_endOnLastFrame && reachedEndFrame) Reset();
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

using Microsoft.Xna.Framework;
using Texture = StoneforgeGame.Game.Utilities.Texture;


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
    private bool _isLooping;


    // CONSTRUCTORS
    public Animation(Texture texture, Point startFrame, Point endFrame, int interval, bool isLooping = true) {
        _texture = texture;
        _startFrame = startFrame;
        _endFrame = endFrame;
        _interval = interval;
        _isLooping = isLooping;
    }


    // PROPERTIES



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
        
        if (_columnIndex >= _endFrame.Y && _rowIndex < _endFrame.X || _columnIndex >= _texture.Columns) {
            _columnIndex = _startFrame.Y;
            _rowIndex++;
        }

        if (!_isLooping) {
            _rowIndex = _endFrame.X;
            _columnIndex = _endFrame.Y;
            return;
        }
        
        if (_rowIndex >= _endFrame.X) {
            _rowIndex = _startFrame.X;
            _columnIndex = _startFrame.Y;
        }
    }

    public Rectangle GetFrame(int frameWidth, int frameHeight) {
        return new Rectangle(frameWidth * _columnIndex, frameHeight * _rowIndex, frameWidth, frameHeight);
    }
}

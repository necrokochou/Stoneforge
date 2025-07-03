using Microsoft.Xna.Framework.Graphics;


namespace StoneforgeGame.Game.Graphics;


public class Texture {
    // FIELDS
    private Texture2D _image;
    private int _rows;
    private int _columns;


    // CONSTRUCTORS
    public Texture(Texture2D image, int rows, int columns) {
        _image = image;
        _rows = rows;
        _columns = columns;
    }


    // PROPERTIES
    public Texture2D Image {
        get => _image;
    }
    public int Rows {
        get => _rows;
    }
    public int Columns {
        get => _columns;
    }


    // METHODS
}

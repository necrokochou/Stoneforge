using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Graphics;


namespace StoneforgeGame.Game.Libraries;


public static class AnimationLibrary {
    // FIELDS
    public static Dictionary<string, Animation> BatumbakalAnimations = new Dictionary<string, Animation> {
        {
            "Idle",
            new Animation(
                TextureLibrary.Batumbakal, new Point(0, 0),new Point(0, 11), 1
            )
        }
    };


    // CONSTRUCTORS



    // PROPERTIES



    // METHODS
}

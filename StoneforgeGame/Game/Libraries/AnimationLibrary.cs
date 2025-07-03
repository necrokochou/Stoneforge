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
                TextureLibrary.Batumbakal,
                new Point(0, 0), new Point(0, 11),
                8
            )
        },
        {
            "Jump",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(1, 1), new Point(1, 3),
                8,
                false)
        },
        {
            "Fall",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(1, 5), new Point(1, 7),
                8,
                false)
        },
        {
            "Walk",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(2, 0), new Point(2, 7),
                8
            )
        }
    };


    // CONSTRUCTORS



    // PROPERTIES



    // METHODS
}

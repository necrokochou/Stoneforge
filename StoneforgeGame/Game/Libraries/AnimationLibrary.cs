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
                new Point(1, 1), new Point(1, 2),
                8, true
            )
        },
        {
            "Fall",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(1, 9), new Point(1, 10),
                8, true
            )
        },
        {
            "Walk",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(2, 0), new Point(2, 7),
                8
            )
        },
        {
            "Hit",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(2, 8), new Point(2, 11),
                8, true
            )
        },
        {
            "Attack",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(3, 0), new Point(3, 8),
                4, true
            )       
        },
        {
            "Death",
            new Animation(
                TextureLibrary.Batumbakal,
                new Point(4, 0), new Point(4, 11),
                8, true
            )       
        }
    };


    // CONSTRUCTORS



    // PROPERTIES



    // METHODS
}

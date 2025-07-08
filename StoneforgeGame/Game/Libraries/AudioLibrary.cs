using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace StoneforgeGame.Game.Libraries;


public static class AudioLibrary {
    // FIELDS
    private static ContentManager _content;
    
    public static List<SoundEffect> BatumbakalDamaged = new List<SoundEffect>();
    public static List<SoundEffect> BatumbakalDeath = new List<SoundEffect>();
    public static List<SoundEffect> BatumbakalJump = new List<SoundEffect>();
    public static List<SoundEffect> BatumbakalRespawn = new List<SoundEffect>();
    
    public static Song AttackSFX;
    public static Song CastleMusic1;
    public static Song CastleMusic2;
    public static Song CaveMusic1;
    public static Song LibraryMusic1;
    public static Song TownMusic1;


    // CONSTRUCTORS



    // PROPERTIES
    public static ContentManager Content {
        set => _content = value;
    }


    // METHODS
    public static void Load() {
        BatumbakalDamaged.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalDamaged1"));
        BatumbakalDamaged.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalDamaged2"));
        BatumbakalDamaged.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalDamaged3"));
        BatumbakalDamaged.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalDamaged4"));
        BatumbakalDeath.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalDeath1"));
        BatumbakalDeath.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalDeath2"));
        BatumbakalJump.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalJump1"));
        BatumbakalJump.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalJump2"));
        BatumbakalJump.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalJump3"));
        BatumbakalRespawn.Add(_content.Load<SoundEffect>("Assets/Audio/BatumbakalRespawn1"));
        
        AttackSFX = _content.Load<Song>("Assets/Audio/AttackSFX");
        CastleMusic1 = _content.Load<Song>("Assets/Audio/CastleMusic1");
        CastleMusic2 = _content.Load<Song>("Assets/Audio/CastleMusic2");
        CaveMusic1 = _content.Load<Song>("Assets/Audio/CaveMusic1");
        LibraryMusic1 = _content.Load<Song>("Assets/Audio/LibraryMusic1");
        TownMusic1 = _content.Load<Song>("Assets/Audio/TownMusic1");
    }
}

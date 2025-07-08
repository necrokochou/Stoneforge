using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace StoneforgeGame.Game.Managers;


public static class AudioManager {
    // FIELDS
    private static List<SoundEffectInstance> _playingSounds = new List<SoundEffectInstance>();
    private static Song _currentSong;
    
    
    // CONSTRUCTORS
    
    
    
    // PROPERTIES
    
    
    
    // METHODS
    public static void Play(List<SoundEffect> sounds, float volume = 1, float pitch = 0, float pan = 0) {
        Random rand = new Random();
        
        int index = rand.Next(0, sounds.Count);
        var instance = sounds[index].CreateInstance();
        instance.Volume = volume;
        instance.Pitch = pitch;
        instance.Pan = pan;
        instance.Play();
        _playingSounds.Add(instance);
        _playingSounds.RemoveAll(s => s.State == SoundState.Stopped);
    }

    public static void Play(SoundEffectInstance sound, float volume = 1, float pitch = 0, float pan = 0) {
        sound.Volume = volume;
        sound.Pitch = pitch;
        sound.Pan = pan;
        sound.Play();
        _playingSounds.Add(sound);
        _playingSounds.RemoveAll(s => s.State == SoundState.Stopped);
    }
    
    public static void Play(Song song, float volume = 1f, bool loop = false) {
        _currentSong = song;
        MediaPlayer.Volume = volume;
        MediaPlayer.Play(song);
        MediaPlayer.IsRepeating = loop;
    }

    public static void Stop() {
        if (MediaPlayer.State == MediaState.Playing) {
            MediaPlayer.Stop();
        }
        _currentSong = null;
    }

    public static bool IsPlaying() {
        _playingSounds.RemoveAll(s => s.State == SoundState.Stopped);
        return _playingSounds.Count > 0;
    }
}
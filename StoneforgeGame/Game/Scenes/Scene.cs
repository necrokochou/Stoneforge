﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Scenes.Components;


namespace StoneforgeGame.Game.Scenes;


public abstract class Scene {
    // FIELDS
    // --- INSTANCE FIELDS ---
    protected string Name;
    protected Background Background;
    public bool IsFinished;
    
    // --- STATIC FIELDS ---
    private static Rectangle _window;
    

    // CONSTRUCTORS
    


    // PROPERTIES
    // --- STATIC PROPERTIES ---
    public static Rectangle Window {
        get => _window;
        set => _window = value;
    }


    // METHODS
    // --- INSTANCE METHODS ---
    public abstract void Load();
    
    public abstract void Unload();
    
    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch spriteBatch);

    public string GetName() {
        return Name;
    }
}

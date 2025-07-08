using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Scenes.Stages;


public abstract class Stage : Scene {
    // FIELDS
    protected CollisionManager CollisionManager = new CollisionManager();
    protected ObjectTileManager ObjectTileManager = new ObjectTileManager();
    protected CharacterManager CharacterManager = new CharacterManager();

    protected string Name;
    protected Character Player;
    protected ObjectTile Objective;

    protected Gravity Gravity;

    // protected Rectangle PreviousSceneBounds;
    protected Rectangle NextSceneBounds;
    // protected bool ReachedPreviousLocation;
    protected bool ReachedNextLocation;
    
    private bool _debug = true;


    // CONSTRUCTORS
    


    // PROPERTIES
    public ObjectTileManager GetObjectTileManager {
        get => ObjectTileManager;
    }
    public CharacterManager GetCharacterManager {
        get => CharacterManager;
    }


    // METHODS
    public override void Unload() {
        CharacterManager.Unload();
        CollisionManager.Unload();
        ObjectTileManager.Unload();
        Background = null;
    }

    public override void Update(GameTime gameTime) {
        CharacterManager.Update(gameTime, Gravity);
        CollisionManager.Update();
        ObjectTileManager.Update();

        var charactersToRemove = new List<Character>();

        foreach (Character character in CharacterManager.Characters) {
            if (character.IsDead) {
                CollisionManager.Remove(character.GetCollisionBox());
                charactersToRemove.Add(character);
            }
        }

        foreach (Character character in charactersToRemove) {
            CharacterManager.Remove(character);
        }
        
        // ReachedPreviousLocation = PreviousSceneBounds.Contains(Player.GetCollisionBox().Bounds);

        ReachedNextLocation = NextSceneBounds.Contains(Player.GetCollisionBox().Bounds);

        if (ReachedNextLocation) {
            Player.ActualPosition = Vector2.Zero;
        }
    }

    
    public override void Draw(SpriteBatch spriteBatch) {
        Background.Draw(spriteBatch);
        if (_debug) CollisionManager.Draw(spriteBatch);
        ObjectTileManager.Draw(spriteBatch);
        CharacterManager.Draw(spriteBatch);
    }

    public CollisionManager GetCollisionManager() {
        return CollisionManager;
    }
    
    public string GetName() {
        return Name;
    }
    
    // public Rectangle GetPreviousSceneBounds() {
    //     return PreviousSceneBounds;
    // }
    //
    // public bool GetReachedPreviousLocation() {
    //     return ReachedPreviousLocation;
    // }
    
    public Rectangle GetNextSceneBounds() {
        return NextSceneBounds;
    }
    
    public bool GetReachedNextLocation() {
        return ReachedNextLocation;
    }

    public ObjectTile GetObjective() {
        return Objective;
    }

    public Character GetPlayer() {
        return Player;
    }

    protected void Save() {
        SaveData saveData = new SaveData {
            CurrentScene = Name,
            CurrentHealth = Player.GetHealth().Current,
            MaximumHealth = Player.GetHealth().Maximum,
            PositionX = Player.ActualPosition.X,
            PositionY = Player.ActualPosition.Y
        };
        SaveManager.Save(saveData);
    }
}

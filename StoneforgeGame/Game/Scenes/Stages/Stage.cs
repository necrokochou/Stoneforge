using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StoneforgeGame.Game.Data;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Scenes.Stages;


public abstract class Stage : Scene {
    // FIELDS
    protected CollisionManager CollisionManager = new CollisionManager();
    protected ObjectTileManager ObjectTileManager = new ObjectTileManager();
    protected CharacterManager CharacterManager = new CharacterManager();

    protected Character Player;
    protected ObjectTile Objective;

    protected Gravity Gravity;

    // protected Rectangle PreviousSceneBounds;
    protected Rectangle NextSceneBounds;
    // protected bool ReachedPreviousLocation;
    protected bool ReachedNextLocation;

    
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
            if (!character.IsAlive && Player != character) {
                CollisionManager.Remove(character.GetCollisionBox());
                charactersToRemove.Add(character);
            }
        }

        foreach (Character character in charactersToRemove) {
            CharacterManager.DeadCharacters.Add(character);
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
        if (MyDebug.IsDebug) CollisionManager.Draw(spriteBatch);
        ObjectTileManager.Draw(spriteBatch);
        CharacterManager.Draw(spriteBatch);
    }

    public CollisionManager GetCollisionManager() {
        return CollisionManager;
    }
    
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

    public void Save() {
        SaveData saveData = new SaveData();
        
        saveData.CurrentScene = Name;
        saveData.PositionX = Player.ActualPosition.X;
        saveData.PositionY = Player.ActualPosition.Y;
        saveData.IsFacingRight = Player.IsFacingRight;
        saveData.CurrentHealth = Player.GetHealth().Current;
        saveData.MaximumHealth = Player.GetHealth().Maximum;
        saveData.GemCount = Player.GetGemCount();
        if (Objective != null)
            saveData.ObjectiveComplete = Objective.IsCompleted;
        saveData.DefeatedEnemies = new List<string>();
        foreach (Character enemy in CharacterManager.DeadCharacters) {
            saveData.DefeatedEnemies.Add(enemy.UniqueID);
        }
        saveData.DestroyedObjectTiles = new List<string>();
        foreach (ObjectTile obj in ObjectTileManager.DestroyedObjectTiles) {
            saveData.DestroyedObjectTiles.Add(obj.UniqueID);
        }
        
        SaveManager.Save(saveData);
    }
}

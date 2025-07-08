using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Data;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Components;
using StoneForgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Scenes.Stages;


public class StageThree : Stage {
    // FIELDS


    
    // CONSTRUCTORS
    public StageThree(Character character) {
        Name = "Forgotten Library";
        Player = character;
        
        ReachedNextLocation = false;
        
        Objective = new Altar(this);
    }


    // PROPERTIES
    


    // METHODS
    public override void Load() {
        SaveData saveData = SaveManager.Load();
        if (saveData == null) {
            saveData = new SaveData();
            saveData.DefeatedEnemies = new List<string>();
            saveData.DestroyedObjectTiles = new List<string>();
        }
        
        Background = new Background(TextureLibrary.StageThreeBackground, Window.Size);
        
        Gravity = new Gravity(
            magnitude: 980f,
            direction: new Vector2(0, 1)
        );
        
        if (Player.ActualPosition == Vector2.Zero) {
            Player.Load(new Point(1680, -100));
        } else {
            Player.Load(Player.ActualPosition.ToPoint());
        }
        CharacterManager.Add(Player);
        
        Enemy skeleton1 = new Skeleton();
        skeleton1.Load(new Point(595, 155));
        skeleton1.PatrolPoints = [595, 925];
        skeleton1.UniqueID = "stage3_skeleton1";
        if (!saveData.DefeatedEnemies.Contains(skeleton1.UniqueID))
            CharacterManager.Add(skeleton1);
        
        Enemy skeleton2 = new Skeleton();;
        skeleton2.Load(new Point(550, 875));
        skeleton2.PatrolPoints = [550, 1000];
        skeleton2.UniqueID = "stage3_skeleton2";
        if (!saveData.DefeatedEnemies.Contains(skeleton2.UniqueID))
            CharacterManager.Add(skeleton2);
        
        CollisionManager.AddRange(CharacterManager.Characters);
        
        CollisionManager.SetBorder(thickness : 90, bottom : true);
        CollisionManager.SetBorder(thickness: 96, left : true, right : true);
        CollisionManager.Add(new Point(0, 0), new Point(1632, 90), ignore : true);

        CollisionManager.Add(new Point(288, 180), new Point(576, 270));
        CollisionManager.Add(new Point(384, 270), new Point(1824, 360));
        CollisionManager.Add(new Point(384, 360), new Point(576, 810));
        CollisionManager.Add(new Point(96, 360), new Point(192, 450));
        CollisionManager.Add(new Point(288, 540), new Point(384, 630));
        CollisionManager.Add(new Point(96, 720), new Point(192, 810));
        CollisionManager.Add(new Point(96, 900), new Point(480, 990));
        CollisionManager.Add(new Point(1248, 900), new Point(1344, 990));
        CollisionManager.Add(new Point(1344, 810), new Point(1440, 990));
        CollisionManager.Add(new Point(1440, 720), new Point(1536, 990));
        CollisionManager.Add(new Point(1536, 630), new Point(1824, 990));
        
        ObjectTile rockPile1 = new RockPile(this);
        rockPile1.Load(new Point(114, 844));
        rockPile1.UniqueID = "stage3_rockPile1";
        if (!saveData.DestroyedObjectTiles.Contains(rockPile1.UniqueID))
            ObjectTileManager.Add(rockPile1);
        
        ObjectTile rockPile2 = new RockPile(this);
        rockPile2.Load(new Point(1282, 214));
        rockPile2.UniqueID = "stage3_rockPile2";
        if (!saveData.DestroyedObjectTiles.Contains(rockPile2.UniqueID))
            ObjectTileManager.Add(rockPile2);
        
        if (!saveData.ObjectiveComplete) {
            ObjectTileManager.Add(Objective, new Point(1582, 380));
        }
        
        CollisionManager.AddRange(ObjectTileManager.ObjectTiles);
        
        CharacterManager.Load(this);

        // PreviousSceneBounds = new Rectangle(new Point(0, 0), new Point(1920, 0));
        NextSceneBounds = new Rectangle(0, 0, 0, 0);
        AudioManager.Play(AudioLibrary.LibraryMusic1, volume : 0.15f, loop : true);
        Save();
    }
}

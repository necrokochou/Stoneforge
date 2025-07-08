using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Data;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Entities.ObjectTiles;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Components;


namespace StoneforgeGame.Game.Scenes.Stages;


public class StageOne : Stage {
    // FIELDS


    
    // CONSTRUCTORS
    public StageOne(Character character) {
        Name = "Heart of the Mountain";
        Player = character;
        
        ReachedNextLocation = false;
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
        
        Background = new Background(TextureLibrary.StageOneBackground, Window.Size);
        
        Gravity = new Gravity(
            magnitude: 980f,
            direction: new Vector2(0, 1)
        );
        
        if (Player.ActualPosition == Vector2.Zero) {
            Player.Load(new Point(200, 800));
        } else {
            Player.Load(Player.ActualPosition.ToPoint());
        }
        CharacterManager.Add(Player);
        
        Enemy skeleton1 = new Skeleton();
        skeleton1.Load(new Point(50, 370));
        skeleton1.PatrolPoints = [50, 700];
        skeleton1.UniqueID = "stage1_skeleton1";
        if (!saveData.DefeatedEnemies.Contains(skeleton1.UniqueID))
            CharacterManager.Add(skeleton1);
        
        CollisionManager.AddRange(CharacterManager.Characters);
        
        CollisionManager.SetBorder(bottom : true, left : true);
        CollisionManager.Add(new Point(1920, 408), new Point(1920, 1080), ignore : true);
        
        CollisionManager.Add(new Point(0, 945), new Point(1920, 1080));
        CollisionManager.Add(new Point(0, 495), new Point(864, 543));
        CollisionManager.Add(new Point(864, 585), new Point(1056, 633));
        CollisionManager.Add(new Point(1056, 675), new Point(1248, 723));
        CollisionManager.Add(new Point(1056, 228), new Point(1152, 276));
        CollisionManager.Add(new Point(1248, 228), new Point(1344, 276));
        CollisionManager.Add(new Point(1440, 228), new Point(1536, 276));
        CollisionManager.Add(new Point(1664, 228), new Point(2120, 276));
        
        ObjectTile rockPile1 = new RockPile(this);
        rockPile1.Load(new Point(1728, 886));
        rockPile1.UniqueID = "stage1_rockPile1";
        if (!saveData.DestroyedObjectTiles.Contains(rockPile1.UniqueID))
            ObjectTileManager.Add(rockPile1);
        
        CollisionManager.AddRange(ObjectTileManager.ObjectTiles);
        
        CharacterManager.Load(this);
        
        // PreviousSceneBounds = new Rectangle(Point.Zero, Point.Zero);
        NextSceneBounds = new Rectangle(new Point(1920, 0), new Point(2120, 1080));
        AudioManager.Play(AudioLibrary.CaveMusic1, volume : 0.1f, loop : true);
        Save();
    }
}

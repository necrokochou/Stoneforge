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


namespace StoneforgeGame.Game.Scenes.Stages;


public class StageTwo : Stage {
    // FIELDS



    // CONSTRUCTORS
    public StageTwo(Character character) {
        Name = "Ruins of Dusk";
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
        
        Background = new Background(TextureLibrary.StageTwoBackground, Window.Size);
        
        Gravity = new Gravity(
            magnitude: 980f,
            direction: new Vector2(0, 1)
        );
        
        if (Player.ActualPosition == Vector2.Zero) {
            Player.Load(new Point(0, 180));
        } else {
            Player.Load(Player.ActualPosition.ToPoint());
        }
        CharacterManager.Add(Player);
        
        Enemy skeleton1 = new Skeleton();
        skeleton1.Load(new Point(528, 315));
        skeleton1.PatrolPoints = [525, 825];
        skeleton1.UniqueID = "stage2_skeleton1";
        if (!saveData.DefeatedEnemies.Contains(skeleton1.UniqueID))
            CharacterManager.Add(skeleton1);
        
        Enemy skeleton2 = new Skeleton();
        skeleton2.Load(new Point(324, 877));
        skeleton2.PatrolPoints = [325, 700];
        skeleton2.UniqueID = "stage2_skeleton2";
        if (!saveData.DefeatedEnemies.Contains(skeleton2.UniqueID))
            CharacterManager.Add(skeleton2);
        
        Enemy skeleton3 = new Skeleton();
        skeleton3.Load(new Point(950, 877));
        skeleton3.PatrolPoints = [950, 1500];
        skeleton3.UniqueID = "stage2_skeleton3";
        if (!saveData.DefeatedEnemies.Contains(skeleton3.UniqueID))
            CharacterManager.Add(skeleton3);
        
        CollisionManager.AddRange(CharacterManager.Characters);
        
        CollisionManager.SetBorder(thickness: 90, top: true);
        CollisionManager.SetBorder(thickness : 96, right : true);
        CollisionManager.Add(new Point(0, 0), new Point(1920, 90), ignore : true);
        CollisionManager.Add(new Point(0, 990), new Point(1632, 1080), ignore : true);
        CollisionManager.Add(new Point(0, 360), new Point(96, 1080), ignore : true);
        CollisionManager.Add(new Point(-200, 270), new Point(0, 270), ignore : true);

        CollisionManager.Add(new Point(0, 270), new Point(384, 360));
        CollisionManager.Add(new Point(384, 270), new Point(480, 450));
        CollisionManager.Add(new Point(384, 450), new Point(1056, 540));
        CollisionManager.Add(new Point(672, 90), new Point(768, 360));
        CollisionManager.Add(new Point(960, 270), new Point(1056, 450));
        CollisionManager.Add(new Point(1056, 270), new Point(1632, 360));
        CollisionManager.Add(new Point(384, 720), new Point(1824, 810));
        CollisionManager.Add(new Point(1440, 630), new Point(1824, 720));
        
        ObjectTile rockPile1 = new RockPile(this);
        rockPile1.Load(new Point(97, 934));
        rockPile1.UniqueID = "stage2_rockPile1";
        if (!saveData.DestroyedObjectTiles.Contains(rockPile1.UniqueID))
            ObjectTileManager.Add(rockPile1);
        
        ObjectTile rockPile2 = new RockPile(this);
        rockPile2.Load(new Point(1715, 573));
        rockPile2.UniqueID = "stage2_rockPile2";
        if (!saveData.DestroyedObjectTiles.Contains(rockPile2.UniqueID))
            ObjectTileManager.Add(rockPile2);
        
        CollisionManager.AddRange(ObjectTileManager.ObjectTiles);
        
        CharacterManager.Load(this);

        // PreviousSceneBounds = new Rectangle(new Point(-200, 0), new Point(0, 1080));
        NextSceneBounds = new Rectangle(new Point(0, 1080), new Point(1920, 1280));
        AudioManager.Play(AudioLibrary.CastleMusic2, volume : 0.1f, loop : true);
        Save();
    }
}

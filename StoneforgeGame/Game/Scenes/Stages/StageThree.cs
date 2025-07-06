using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Components;


namespace StoneforgeGame.Game.Scenes.Stages;


public class StageThree : Stage {
    // FIELDS


    
    // CONSTRUCTORS
    public StageThree(Character character) {
        Name = "Heart of the Mountain";
        Player = character;
        
        ReachedNextLocation = false;
    }


    // PROPERTIES
    


    // METHODS
    public override void Load() {
        Background = new Background(TextureLibrary.StageThreeBackground, Window.Size);
        
        Gravity = new Gravity(
            magnitude: 980f,
            direction: new Vector2(0, 1)
        );
        
        Player.Load(Window, new Point(1728, -100));
        CharacterManager.Add(Player);
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
        
        CharacterManager.Load(this);

        NextSceneBounds = new Rectangle(0, 0, 0, 0);
    }
}

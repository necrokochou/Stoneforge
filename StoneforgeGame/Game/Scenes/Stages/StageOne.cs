using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Entities.Characters;
using StoneforgeGame.Game.Libraries;
using StoneforgeGame.Game.Managers;
using StoneForgeGame.Game.Managers;
using StoneforgeGame.Game.Physics;
using StoneforgeGame.Game.Scenes.Components;


namespace StoneforgeGame.Game.Scenes.Stages;


public class StageOne : Stage {
    // FIELDS


    
    // CONSTRUCTORS
    public StageOne(Character character) {
        CollisionManager = new CollisionManager();
        Name = "Heart of the Mountain";
        Player = character;
        
        ReachedNextLocation = false;
        
        Gravity = new Gravity(
            magnitude: 980f,
            direction: new Vector2(0, 1)
        );
    }


    // PROPERTIES
    


    // METHODS
    public override void Load() {
        Background = new Background(TextureLibrary.StageOneBackground, Window.Size);
        
        CollisionManager.SetBorder(top : true, bottom : true, left : true);
        CollisionManager.Add(new Point(1920, 408), new Point(1920, 1080), ignore : true);
        
        CollisionManager.Add(new Point(0, 945), new Point(1920, 1080));
        CollisionManager.Add(new Point(0, 495), new Point(864, 543));
        CollisionManager.Add(new Point(864, 585), new Point(1056, 633));
        CollisionManager.Add(new Point(1056, 675), new Point(1248, 723));
        CollisionManager.Add(new Point(1056, 312), new Point(1152, 408));
        CollisionManager.Add(new Point(1248, 312), new Point(1344, 408));
        CollisionManager.Add(new Point(1440, 312), new Point(1536, 408));
        CollisionManager.Add(new Point(1664, 312), new Point(2120, 408));
        
        Player.Load(Window, new Point(200, 800));
        CollisionManager.Add(Player.Collider);
        CharacterManager = new CharacterManager(CollisionManager);
        CharacterManager.Add(Player);

        NextSceneBounds = new Rectangle(new Point(1920, 0), new Point(2120, 1080));
    }
}

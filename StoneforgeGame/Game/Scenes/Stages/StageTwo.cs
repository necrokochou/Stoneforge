using Microsoft.Xna.Framework;
using StoneforgeGame.Game.Entities.Characters;
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
        CollisionManager = new CollisionManager();
        Name = "Heart of the Mountain";
        Player = character;

        ReachedNextLocation = false;

        Gravity = new Gravity(
            magnitude : 980f,
            direction : new Vector2(0, 1)
        );
    }


    // PROPERTIES



    // METHODS
    public override void Load() {
        Background = new Background(TextureLibrary.StageTwoBackground, Window.Size);

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

        Player.Load(Window, new Point(-100, 180));
        CollisionManager.Add(Player.Collider);
        CharacterManager = new CharacterManager(CollisionManager);
        CharacterManager.Add(Player);

        NextSceneBounds = new Rectangle(new Point(0, 1080), new Point(1920, 1280));
    }
}

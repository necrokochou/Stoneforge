using System;
using System.Collections.Generic;


namespace StoneforgeGame.Game.Data;


[Serializable]
public class SaveData {
    // FIELDS
    public string CurrentScene { get; set; }
    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public bool IsFacingRight { get; set; }
    public float CurrentHealth { get; set; }
    public float MaximumHealth { get; set; }
    public int GemCount { get; set; }
    public bool ObjectiveComplete { get; set; }
    public List<string> DefeatedEnemies { get; set; }
    public List<string> DestroyedObjectTiles { get; set; }


    // CONSTRUCTORS



    // PROPERTIES



    // METHODS
}

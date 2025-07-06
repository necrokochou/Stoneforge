using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using StoneforgeGame.Game.Utilities;


namespace StoneforgeGame.Game.Managers;


public static class SaveManager {
    // FIELDS
    private static readonly string _savePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Stoneforge", "save.json"
    );


    // CONSTRUCTORS



    // PROPERTIES



    // METHODS
    public static void Save(SaveData saveData) {
        try {
            string directory = Path.GetDirectoryName(_savePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_savePath, json);
            Console.WriteLine("Game saved.");
        } catch (Exception ex) {
            Console.WriteLine("Save failed: " + ex.Message);
        }
    }

    public static SaveData Load() {
        try {
            if (!File.Exists(_savePath)) return null;

            string json = File.ReadAllText(_savePath);
            SaveData saveData = JsonSerializer.Deserialize<SaveData>(json);
            Console.WriteLine("Game loaded.");
            return saveData;
        } catch (Exception ex) {
            Console.WriteLine("Load failed: " + ex.Message);
            return null;
        }
    }

    public static void DeleteSave() {
        if (File.Exists(_savePath))
            File.Delete(_savePath);
    }
}

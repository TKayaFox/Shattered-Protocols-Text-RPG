using System;
using System.IO;
using System.Text.Json;

namespace Shattered_Protocols
{
    public static class SaveSystem
    {
        private static string saveFilePath = "game_save.json";

        // Save the current game state to a JSON file
        public static void SaveGame(GameState gameState)
        {
            try
            {
                string json = JsonSerializer.Serialize(gameState, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(saveFilePath, json);
                Console.WriteLine("Game saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving game: {ex.Message}");
            }
        }

        // Load the game state from a JSON file
        public static GameState LoadGame()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    string json = File.ReadAllText(saveFilePath);
                    GameState gameState = JsonSerializer.Deserialize<GameState>(json);
                    Console.WriteLine("Game loaded successfully!");
                    return gameState;
                }
                else
                {
                    Console.WriteLine("No save file found. Starting a new game.");
                    return new GameState();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading game: {ex.Message}");
                return new GameState(); // Return default state on failure
            }
        }
    }
}

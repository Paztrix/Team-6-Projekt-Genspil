using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Genspil
{
    public static class DataHandler
    {
        private static string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Data");

        // Tillader at sætte ny Data-mappe dynamisk, f.eks. til test
        public static void SetDataFolder(string folderPath)
        {
            dataFolder = folderPath;
            Directory.CreateDirectory(dataFolder);
        }
        // Genskaber standardmappen (f.eks. til nulstilling efter test)
        public static void ResetDataFolder()
        {
            dataFolder = Path.Combine(AppContext.BaseDirectory, "Data");
            Directory.CreateDirectory(dataFolder);
        }

        // --- GameTypes ---
        public static void SameGameTypes(List<GameType> descriptions)
        {
            string path = Path.Combine(dataFolder, "gametypes.txt");
            using StreamWriter writer = new StreamWriter(path);
            foreach (var desc in descriptions)
            {
                writer.WriteLine(desc.ToString());
            }
        }

        public static List<GameType> LoadGameTypes()
        {
            string path = Path.Combine(dataFolder, "gametypes.txt");
            List<GameType> result = new();
            if (!File.Exists(path)) return result;

            using StreamReader reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                result.Add(GameType.FromString(line));
            }

            return result;
        }

        // --- Games ---
        public static void SaveGames(List<Game> games)
        {
            string path = Path.Combine(dataFolder, "games.txt");
            using StreamWriter writer = new StreamWriter(path);
            foreach (var game in games)
            {
                writer.WriteLine(game.ToString());
            }
        }

        public static List<Game> LoadGames(List<GameType> descriptions)
        {
            string path = Path.Combine(dataFolder, "games.txt");
            List<Game> result = new();
            if (!File.Exists(path)) return result;

            using StreamReader reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                result.Add(Game.FromString(line, descriptions));
            }

            return result;
        }

        // --- Requests ---
        public static void SaveRequests(List<Request> requests)
        {
            string path = Path.Combine(dataFolder, "requests.txt");
            using StreamWriter writer = new StreamWriter(path);
            foreach (var req in requests)
            {
                writer.WriteLine(req.ToString());
            }
        }

        public static List<Request> LoadRequests()
        {
            string path = Path.Combine(dataFolder, "requests.txt");
            List<Request> result = new();
            if (!File.Exists(path)) return result;

            using StreamReader reader = new StreamReader(path);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                result.Add(Request.FromString(line));
            }

            return result;
        }
    }
}

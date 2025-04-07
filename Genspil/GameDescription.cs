using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Genspil
{
    public class GameDescription
    {
        // Basisoplysninger
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinAge { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public Genre GameGenre { get; set; }

        public GameDescription(string name, string description = "", int minAge = 0, int minPlayers = 0, int maxPlayers = 99, Genre gameGenre = Genre.NA)
        {
            this.Name = name;
            this.Description = description;
            this.MinAge = minAge;
            this.MinPlayers = minPlayers;
            this.MaxPlayers = maxPlayers;
            this.GameGenre = gameGenre;
        }
        public override string ToString()
        {
            return $"{Name};{Description};{MinAge};{MinPlayers};{MaxPlayers};{GameGenre}";
        }

        public static GameDescription FromString(string line)
        {
            var parts = line.Split(';');
            return new GameDescription(
                parts[0],                     // Name
                parts[1],                     // Description
                int.Parse(parts[2]),          // MinAge
                int.Parse(parts[3]),          // MinPlayers
                int.Parse(parts[4]),          // MaxPlayers
                Enum.Parse<Genre>(parts[5])   // GameGenre
            );
        }

        public static void DisplayGameDescription(List<GameDescription> gameDescriptions)
        {
            //Console.WriteLine($"{Name} ({GameGenre}), {Description} - from {MinPlayers}-{MaxPlayers}, minimum age {MinAge} ");


        }

        /*
        public static void DisplayGames(List<Game> games)
        {
            // Definerer hver kolonnes bredde
            int col1Width = 16, col2Width = 10, col3Width = 8, col4Width = 15, col5Width = 5, col6Width = 10;

            // Beregner den totale bredde for separatorlinjen
            int totalWidth = col1Width + col2Width + col3Width + col4Width + col5Width + col6Width + 19;
            string separator = new string('-', totalWidth);

            // Printer header
            Console.WriteLine(separator);
            Console.WriteLine($"| {"Brætspil".PadRight(col1Width)} | {"Genre".PadRight(col2Width)} | {"Pris".PadRight(col3Width)} | {"Antal Spillere".PadRight(col4Width)} | {"Alder".PadRight(col5Width)} | {"Stand".PadRight(col6Width)} |");
            Console.WriteLine(separator);

            // Udskriv hvert spil i tabelformat
            foreach (var game in games)
            {
                string name = game.type.Name.PadRight(col1Width);
                string genre = game.type.GameGenre.ToString().PadRight(col2Width);
                string price = game.price.ToString("F2").PadRight(col3Width);
                string players = $"{game.type.MinPlayers}-{game.type.MaxPlayers}".PadRight(col4Width);
                string age = game.type.MinAge.ToString().PadRight(col5Width);
                string condition = game.GameCondition.ToString().PadRight(col6Width);

                Console.WriteLine($"| {name} | {genre} | {price} | {players} | {age} | {condition} |");
            }

            Console.WriteLine(separator);
        }
        */
    }
}
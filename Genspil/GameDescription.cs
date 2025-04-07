using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            List<Game> games = DataHandler.LoadGames(gameDescriptions);
            List<Request> requests = DataHandler.LoadRequests();

            // Definerer hver kolonnes bredde
            int col1Width = 16, col2Width = 10, col3Width = 15, col4Width = 15;

            // Beregner den totale bredde for separatorlinjen
            int totalWidth = col1Width + col2Width + col3Width + col4Width + 19;
            string separator = new string('-', totalWidth);

            // Printer header
            Console.WriteLine(separator);
            Console.WriteLine($"| {"Brætspil".PadRight(col1Width)} | {"Genre".PadRight(col2Width)} | {"Antal på lager".PadRight(col3Width)} | {"Forespørgsler".PadRight(col4Width)} |");
            Console.WriteLine(separator);


            foreach (var gt in gameDescriptions)
            {
                int gameCount = 0;
                foreach (Game game in games) 
                {
                    if (gt == game.type) gameCount++;    
                }

                int reqCount = 0;
                foreach (Request request in requests)
                {
                    if (gt.Name == request.RequestedGame) reqCount++;
                }


                string name = gt.Name.PadRight(col1Width);
                string genre = gt.GameGenre.ToString().PadRight(col2Width);
                string gameCountFormated = gameCount.ToString().PadRight(col3Width);
                string ReqCountFormated = reqCount.ToString().PadRight(col4Width);

                Console.WriteLine($"| {name} | {genre} | {gameCountFormated} | {ReqCountFormated} |");
            }
            Console.WriteLine(separator);

        }

      
    }
}
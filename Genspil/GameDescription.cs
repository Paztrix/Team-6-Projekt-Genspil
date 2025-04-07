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

        public void DisplayGameDescription()
        {
            Console.WriteLine($"{Name} ({GameGenre}), {Description} - from {MinPlayers}-{MaxPlayers}, minimum age {MinAge} ");
        }
    }
}

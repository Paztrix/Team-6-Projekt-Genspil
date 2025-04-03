using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public class GameType
    {
        // Basisoplysninger
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public Genre GameGenre { get; set; }

        public GameType(string Name, string Description = "", int MinAge = 0, int minPlayers = 0, int maxPlayers = 99, Genre genre = Genre.NA)
        {
            this.Name = Name;
            this.Description = Description;
            this.Age = MinAge;
            this.MinPlayers = minPlayers;
            this.MaxPlayers = maxPlayers;
            this.GameGenre = genre;
        }

        public void DisplayGameType()
        {
            Console.WriteLine($"{Name} ({GameGenre}), {Description} - from {MinPlayers}-{MaxPlayers}, minimum age {Age} ");
        }
    }
}

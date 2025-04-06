using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public static class PseudoDatabase
    {
        public static List<Game> games = new List<Game>();
        public static List<Request> requests = new List<Request>();
        public static List<GameDescription> gametypes = new List<GameDescription>();

        public static void DatabaseSeeder()
        {
            gametypes.Add(new GameDescription("Jumanji", "Farligt spil i junglen", 12, 3, 9, Genre.Campaign));
            gametypes.Add(new GameDescription("Monopoly", "", 8, 4, 6, Genre.Familygame));
            gametypes.Add(new GameDescription("Ludo", "", 4, 2, 4, Genre.Familygame));

            games.Add(new Game(1, 50.00, gametypes[0], Condition.Fine));
            games.Add(new Game(2, 75.00, gametypes[0], Condition.Perfect));
            games.Add(new Game(3, 99.00, gametypes[1], Condition.InWorkshop));
            games.Add(new Game(4, 20.00, gametypes[2], Condition.Unplayable));
            games.Add(new Game(5, 40.00, gametypes[2], Condition.Perfect));

            requests.Add(new Request(1, DateTime.Now.AddDays(-2), "John Doe", "1234567890", "johndoe@email.com", "Skak"));
            requests.Add(new Request(2, DateTime.Now.AddDays(-1), "Jane Smith", "9876543210", "janesmith@email.com", "Backgammon"));
            requests.Add(new Request(3, DateTime.Now, "Alice Johnson", "5551234567", "alicejohnson@email.com", "UNO"));
        }
    }
}

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
        public static List<GameType> gametypes = new List<GameType>();

        public static void DatabaseSeeder()
        {
            gametypes.Add(new GameType("Jumanji", "Farligt spil i junglen", 12, 3, 9, Genre.Kampagne));
            gametypes.Add(new GameType("Monopoly", "", 8, 4, 6, Genre.Familie));
            gametypes.Add(new GameType("Ludo", "", 4, 2, 4, Genre.Familie));

            games.Add(new Game(1, 50.00, gametypes[0], Condition.Brugsspor));
            games.Add(new Game(2, 75.00, gametypes[0], Condition.Perfekt));
            games.Add(new Game(3, 99.00, gametypes[1], Condition.Reperation));
            games.Add(new Game(4, 20.00, gametypes[2], Condition.Slidt));
            games.Add(new Game(5, 40.00, gametypes[2], Condition.Perfekt));

            requests.Add(new Request(1, DateTime.Now.AddDays(-2), "John Doe", "1234567890", "johndoe@email.com", "Skak"));
            requests.Add(new Request(2, DateTime.Now.AddDays(-1), "Jane Smith", "9876543210", "janesmith@email.com", "Backgammon"));
            requests.Add(new Request(3, DateTime.Now, "Alice Johnson", "5551234567", "alicejohnson@email.com", "UNO"));
        }
    }
}

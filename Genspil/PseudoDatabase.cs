using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public static class PsudoDatabase
    {
        public static List<Game> games = new List<Game>();
        public static List<Request> requests = new List<Request>();
        public static List<GameType> gametypes = new List<GameType>();

        public static void DatabaseSeeder()
        {
            gametypes.Add(new GameType(Name: "Jumanji", Description: "Farligt spil i junglen", MinAge: 12, minPlayers: 3, maxPlayers: 9, genre: Genre.Campaign));
            gametypes.Add(new GameType(Name: "Monopoli", Description: "", MinAge: 8, minPlayers: 4, maxPlayers: 6, genre: Genre.Familygame));
            gametypes.Add(new GameType(Name: "Ludo", Description: "", MinAge: 4, minPlayers: 2, maxPlayers: 4, genre: Genre.Familygame));

            games.Add(new Game(id: 1, price: 75.00, gametype: gametypes[0], condition: Condition.Fine));
            games.Add(new Game(id: 2, price: 50.00, gametype: gametypes[0], condition: Condition.Perfect));
            games.Add(new Game(id: 3, price: 99.00, gametype: gametypes[1], condition: Condition.InWorkshop));
            games.Add(new Game(id: 4, price: 20.00, gametype: gametypes[2], condition: Condition.Unplayble));
            games.Add(new Game(id: 5, price: 40.00, gametype: gametypes[2], condition: Condition.Perfect));

            requests.Add(new Request(1, DateTime.Now.AddDays(-2), "John Doe", "1234567890", "johndoe@email.com", gametypes[2]));
            requests.Add(new Request(2, DateTime.Now.AddDays(-1), "Jane Smith", "9876543210", "janesmith@email.com", gametypes[0]));
            requests.Add(new Request(3, DateTime.Now, "Alice Johnson", "5551234567", "alicejohnson@email.com", gametypes[1]));

        }
    }
}

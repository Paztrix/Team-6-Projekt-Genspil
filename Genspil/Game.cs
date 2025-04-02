using System;
using System.Collections.Generic;

namespace Genspil
{
    public class Game
    {
        private int id;
        public double price { get; private set; }
        public Condition GameCondition { get; private set; }
        public GameType type { get; private set; }

        public Game(int id, double price, GameType gametype, Condition condition = Condition.Perfect)
        {
            this.id = id;
            this.price = price;
            this.GameCondition = condition;
            this.type = gametype;
        }

        public static void DisplayGames()
        {
            Console.WriteLine("Liste over oprettede spil:");
            foreach (var game in PseudoDatabase.games)
            {
                Console.WriteLine($"Name: {game.type.Name}, Price: {game.price}, Condition: {game.GameCondition}, Genre: {game.type.GameGenre}, Age: {game.type.Age}, Min Players: {game.type.MinPlayers}, Max Players: {game.type.MaxPlayers}, Description: {game.type.Description}");
            }
        }

        public void Checkout() { }
    }
}

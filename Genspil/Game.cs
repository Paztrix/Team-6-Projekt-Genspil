using System;
using System.Collections.Generic;
using Genspil;

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

        //public static void DisplayGames()
        //{
        //    Console.WriteLine("Liste over oprettede spil:");
        //    foreach (var game in PseudoDatabase.games)
        //    {
        //        Console.WriteLine($"Name: {game.type.Name}, Genre: {game.type.GameGenre}, Price: {game.price}, Age: {game.type.Age}, Players: {game.type.MinPlayers}-{game.type.MaxPlayers}, Condition: {game.GameCondition}, Description: {game.type.Description}");
        //    }
        //}

        //Display metode der printer spil
        public static void DisplayGame()
        {
            //Definere hver kolonnes bredde: Brætspil | Genre | Pris | Antal Spillere (Min-Max) | Alder | Stand |
            int col1Width = 12, col2Width = 10, col3Width = 8, col4Width = 15, col5Width = 5, col6Width = 10;

            //Beregner den totale bredde for seperator linjen
            int totalWidth = col1Width + col2Width + col3Width + col4Width + col5Width + col6Width + 19;
            string separator = new string('-', totalWidth);

            //Printer en header
            Console.WriteLine(separator);
            Console.WriteLine($"| {"Brætspil".PadRight(col1Width)} | {"Genre".PadRight(col2Width)} | {"Pris".PadRight(col3Width)} | {"Antal Spillere".PadRight(col4Width)} | {"Alder".PadRight(col5Width)} | {"Stand".PadRight(col6Width)} |");
            Console.WriteLine(separator);

            //Itererer gennem alle spil og printer
            foreach (var game in PseudoDatabase.games)
            {
                string playerRange = $"{game.type.MinPlayers}-{game.type.MaxPlayers}";
                Console.WriteLine($"| {game.type.Name.PadRight(col1Width)} | {game.type.GameGenre.ToString().PadRight(col2Width)} | {game.price.ToString().PadRight(col3Width)} | {playerRange.PadRight(col4Width)} | {game.type.Age.ToString().PadRight(col5Width)} | {game.GameCondition.ToString().PadRight(col6Width)} |");
                Console.WriteLine(separator);
            }
        }

        //Checkout metode til at fjerne spil
        public static void Checkout()
        {
            Console.Write("Indtast spillets navn: ");
            string gameName = Console.ReadLine();

            Console.Write("Indtast spillets stand: ");
            string conditionInput = Console.ReadLine();
            if (!Enum.TryParse(conditionInput, out Condition gameCondition))
            {
                Console.WriteLine("Ugyldigt input for spillets stand.");
                return;
            }

            Console.Write("Indtast spillets pris: ");
            double gamePrice = double.Parse(Console.ReadLine());

            //Tjekker om et spil findes med indtastede data og hvis ja, så slet det
            var gameToCheckout = PseudoDatabase.games.FirstOrDefault(g =>
                g.type.Name.Equals(gameName, StringComparison.OrdinalIgnoreCase) &&
                g.GameCondition == gameCondition &&
                g.price == gamePrice);

            if (gameToCheckout != null)
            {
                PseudoDatabase.games.Remove(gameToCheckout);
                Console.WriteLine($"Spillet '{gameToCheckout.type.Name}' er nu tjekket ud og fjernet fra lageret.");
            }
            else
            {
                Console.WriteLine("Ingen spil fundet med de angivne kriterier.");
            }
        }

        public static void SortGames()
        {
            Console.WriteLine("--- Sortere Spil Efter ---");
            Console.WriteLine("1. Sorter efter navn");
            Console.WriteLine("2. Sorter efter stand");
            Console.WriteLine("Indtast valg: ");
            string choice = Console.ReadLine();

            switch(choice)
            {
                case "1":
                    PseudoDatabase.games = PseudoDatabase.games.OrderBy(game => game.type.Name).ToList();
                    break;

                case "2":
                    PseudoDatabase.games = PseudoDatabase.games.OrderBy(game => game.GameCondition).ToList();
                    break;
            }
        }
    }
}

/*
|--------------------------------------------------------------------|
| Brætspil | Genre      | Pris | Antal spillere | Alder | Stand      |
|--------------------------------------------------------------------|
| Jumanji  | Campaign   | 50   | 3-9            | 12    | Fine       |
|--------------------------------------------------------------------|
| Jumanji  | Campaign   | 75   | 3-9            | 12    | Perfect    |
|--------------------------------------------------------------------|
| Monopoly | Familygame | 99   | 4-6            | 8     | InWorkshop |
|--------------------------------------------------------------------|
| Ludo     | Familygame | 20   | 2-4            | 4     | Unplayable |
|--------------------------------------------------------------------|
| Ludo     | Familygame | 40   | 2-4            | 4     | Perfect    |
|--------------------------------------------------------------------|
*/
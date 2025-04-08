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
        // Offentlig read-only property. Viser spillets interne ID, til identifikation og persistens:
        public int Id => id;

        public Game(int id, double price, GameType gametype, Condition condition = Condition.Perfekt)
        {
            this.id = id;
            this.price = price;
            this.GameCondition = condition;
            this.type = gametype;
        }

        //Display metode der printer spil
        public override string ToString()
        {
            return $"{id};{price};{type.Name};{GameCondition}";
        }

        public static Game FromString(string line, List<GameType> descriptions)
        {
            var parts = line.Split(';');
            int id = int.Parse(parts[0]);
            double price = double.Parse(parts[1]);
            string gameTypeName = parts[2];
            Condition condition = Enum.Parse<Condition>(parts[3]);

            var description = descriptions.FirstOrDefault(d => d.Name == gameTypeName)
                ?? throw new Exception($"Gametype '{gameTypeName}' not found.");

            return new Game(id, price, description, condition);
        }

        //Viser en liste af spil formateret i konsollen
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

        //Sortere listen af spil
        public static void SortGames(List<Game> games)
        {
            while(true)
            {
                Console.Write("Sorter spil ( 1. Ja / 2. Nej ): ");
                string sortInput = Console.ReadLine();
                if (sortInput == "1")
                {
                    Console.Clear();
                    Console.WriteLine("--- Sortere Spil Efter ---");
                    Console.WriteLine("1. Sorter efter navn");
                    Console.WriteLine("2. Sorter efter stand");
                    Console.WriteLine("3. Sorter efter genre");
                    Console.Write("Indtast valg: ");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            games = games.OrderBy(game => game.type.Name).ToList();
                            break;

                        case "2":
                            games = games.OrderBy(game => game.GameCondition).ToList();
                            break;

                        case "3":
                            games = games.OrderBy(game => game.type.GameGenre).ToList();
                            break;
                    }
                    DisplayGames(games);
                } else if (sortInput == "2")
                {
                    return;
                }
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
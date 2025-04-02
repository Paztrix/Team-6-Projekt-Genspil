using System;
using System.Collections.Generic;

namespace Genspil
{
    // Klasse til at håndtere forespørgsler
    public class Forespørgsel
    {
        public string SpilNavn { get; set; }
        public int Id { get; set; }
        public string Kommentar { get; set; }

        public Forespørgsel(string spilNavn, int id)
        {
            this.SpilNavn = spilNavn;
            this.Id = id;
        }

        public void VisForespørgsel()
        {
            Console.WriteLine($"SpilNavn: {SpilNavn}, SpilId: {Id}");
        }
    }

    // Klasse til spil
    public class Game
    {
        public string SpilNavn { get; set; }
        public int Id { get; set; }
        public double Price { get; set; }
        public string GameType { get; set; }
        public string GameGenre { get; set; }

        public Game(string spilNavn, int id, double price, string gameType, string gameGenre)
        {
            this.SpilNavn = spilNavn;
            this.Id = id;
            this.Price = price;
            this.GameType = gameType;
            this.GameGenre = gameGenre;
        }

        public void Display()
        {
            Console.WriteLine($"Titel: {SpilNavn}, Id: {Id}, Pris: {Price}, Type: {GameType}, Genre: {GameGenre}");
        }
    }

    // Programklassen med menu og main-metoden
    public class Program
    {
        // Lister til at gemme spil og forespørgsler
        static List<Game> games = new List<Game>();
        static List<Forespørgsel> forespørgsler = new List<Forespørgsel>();

        public static void Main(string[] args)
        {
            // Allerede fyldt data
            games.Add(new Game("Jumanji", 101, 50.95, "Adventure", "Family"));
            forespørgsler.Add(new Forespørgsel("Jumanji", 101));

            ShowMenu();
        }

        public static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nVelkommen til menuen, du har følgende valg muligheder:");
                Console.WriteLine("Tryk på 1 for at oprette et spil");
                Console.WriteLine("Tryk på 2 for at oprette en forespørgsel");
                Console.WriteLine("Tryk på 3 for at få vist oprettede spil");
                Console.WriteLine("Tryk på 4 for at få vist oprettede forespørgsler");
                Console.WriteLine("Tryk på 0 for at afslutte programmet");
                Console.Write("Dit valg: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        CreateGame();
                        break;
                    case "2":
                        CreateForespørgsel();
                        break;
                    case "3":
                        DisplayGames();
                        break;
                    case "4":
                        DisplayForespørgsler();
                        break;
                    case "0":
                        Console.WriteLine("Afslutter programmet");
                        return;
                    default:
                        Console.WriteLine("Ugyldigt input, prøv igen.");
                        break;
                }
            }
        }

        //Oprettelse af spil
        public static void CreateGame()
        {
            Console.Write("Indtast spil navn: ");
            string spilNavn = Console.ReadLine();

            Console.Write("Indtast spil id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Forkert input, spil id skal være et tal. Prøv igen: ");
            }

            Console.Write("Indtast pris: ");
            double price;
            while (!double.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Forkert input, pris skal være et tal. Prøv igen: ");
            }

            Console.Write("Indtast game type: ");
            string gameType = Console.ReadLine();

            Console.Write("Indtast game genre (Familygame / Campaign / Puzzle / NA): ");
            string gameGenre = Console.ReadLine();

            Game newGame = new Game(spilNavn, id, price, gameType, gameGenre);
            games.Add(newGame);
            Console.WriteLine("\nSpil oprettet med følgende værdier:");
            newGame.Display();
        }

        //Oprettelse af forespørgsel
        public static void CreateForespørgsel()
        {
            Console.Write("Indtast spil navn for forespørgsel: ");
            string spilNavn = Console.ReadLine();

            Console.Write("Indtast spil id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Forkert input, spil id skal være et tal. Prøv igen: ");
            }

            Forespørgsel newForespørgsel = new Forespørgsel(spilNavn, id);
            forespørgsler.Add(newForespørgsel);
            Console.WriteLine("\nForespørgsel oprettet:");
            newForespørgsel.VisForespørgsel();
        }

        //Visning af oprettede spil
        public static void DisplayGames()
        {
            Console.WriteLine("Liste over oprettede spil:");
            foreach (Game game in games)
            {
                game.Display();
            }
        }
        //Visning af oprettede forespørgelser
        public static void DisplayForespørgsler()
        {
            Console.WriteLine("Liste over forespørgsler:");
            foreach (Forespørgsel f in forespørgsler)
            {
                f.VisForespørgsel();
            }
        }
    }
}

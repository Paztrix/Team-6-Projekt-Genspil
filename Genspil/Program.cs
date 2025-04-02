namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PsudoDatabase.DatabaseSeeder();

            foreach (var game in PsudoDatabase.games)
            {
                Console.WriteLine($"Name: {game.type.Name}, Price: {game.price}, Condition: {game.GameCondition}, Genre: {game.type.GameGenre}, Age: {game.type.Age}, Min Players: {game.type.MinPlayers}, Max Players: {game.type.MaxPlayers}, Description: {game.type.Description}");
            }

            Request.DisplayRequests();

            Console.Write("Do you want to create a new game?: ");
            string choice = Console.ReadLine();

            if (choice == "yes")
            {
                GameCreation();
            }

            Console.Write("Press key to stop program...");
            Console.ReadLine();
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

        static void GameCreation()
        {
            Console.Write("Indtast spillets navn: ");
            string gameName = Console.ReadLine();


            Console.Write("Indtast beskrivelse: ");
            string gameDesc = Console.ReadLine();


            Console.Write("Indtast minimum alder: ");
            int minAge = int.Parse(Console.ReadLine());


            Console.Write("Indtast minimum spillere: ");
            int minPlayers = int.Parse(Console.ReadLine());


            Console.Write("Indtast maksimum antal spillere: ");
            int maxPlayers = int.Parse(Console.ReadLine());

            GameType newGame = new GameType(gameName, gameDesc, minAge, minPlayers, maxPlayers);

            Console.WriteLine($"Spillet {newGame.Name} er oprettet!");
        }

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

        public static void DisplayGames()
        {
            Console.WriteLine("Liste over oprettede spil:");
            foreach (Game game in games)
            {
                game.Display();
            }
        }
    }
}

/*
Method CreateNewGame():
    //spørg brugeren om spillets navn
    Display ( indtast spillets navn: )
    gameName = userData()

    //spørg brugeren om spillets beskrivelse
    Display ( indtast beskrivelse: )
    gameDesc = userData()

    //spørg brugeren om minimum alder
    Display ( indtast minimum alder: )
    minAge = userData()

    //spørg brugeren om minimum antal spillere
    Display ( indtast minimum spillere: )
    minPlayers = userData()

    //spørg brugeren om maksimum antal spillere
    Display ( indtast maksimum antal spillere: )
    maxPlayers = userData()

    //Opret ny instans af GameType med de indtastede oplysninger
    newGame = new GameType(gameName, gameDesc, minAge, minPlayers, maxPlayers)

    //bekræft spillet er oprettet
    Display ( Spillet {gameName} er oprettet)

    return newGame
*/

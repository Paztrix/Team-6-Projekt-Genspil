namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PseudoDatabase.DatabaseSeeder();

            foreach (var game in PseudoDatabase.games)
            {
                Console.WriteLine($"Name: {game.type.Name}, Price: {game.price}, Condition: {game.GameCondition}, Genre: {game.type.GameGenre}, Age: {game.type.Age}, Min Players: {game.type.MinPlayers}, Max Players: {game.type.MaxPlayers}, Description: {game.type.Description}");
            }

            Request.DisplayRequests();

            ShowMenu();

            Console.Write("Press key to stop program...");
            Console.ReadLine();
        }

        public static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nVelkommen til menuen, du har følgende valg muligheder:");
                Console.WriteLine("1: Opret spil");
                Console.WriteLine("2: Opret forespørgsel");
                Console.WriteLine("3: Vis spil");
                Console.WriteLine("4: Vis forespørgsler");
                Console.WriteLine("0: Afslutte program");
                Console.Write("Dit valg: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        GameCreation();
                        break;
                    case "2":
                        CreateRequest();
                        break;
                    case "3":
                        Game.DisplayGames();
                        break;
                    case "4":
                        Request.DisplayRequests();
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
            PseudoDatabase.gametypes.Add(newGame);

            Console.WriteLine($"Spillet {newGame.Name} er oprettet!");
        }

        public static void CreateRequest()
        {
            Console.Write("Kundenavn: ");
            string customerName = Console.ReadLine();

            Console.Write("Telefon: ");
            string customerNumber = Console.ReadLine();

            Console.Write("Email: ");
            string customerEmail = Console.ReadLine();

            Console.Write("Brætspil: ");
            string requestedGame = Console.ReadLine();

            DateTime requestDate = DateTime.Now;

            Random random = new Random();
            int id = random.Next();

            Request newRequest = new Request(id, requestDate, customerName, customerNumber, customerEmail, requestedGame);
            PseudoDatabase.requests.Add(newRequest);
            Console.WriteLine("\nForespørgsel oprettet:");
            Request.DisplayRequests();
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

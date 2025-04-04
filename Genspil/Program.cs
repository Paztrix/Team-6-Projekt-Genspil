using Genspil;

namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PseudoDatabase.DatabaseSeeder();

            ShowMenu();
        }

        public static void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Genspil Lager System ---");
                Console.WriteLine("1: Opret spil");
                Console.WriteLine("2: Opret forespørgsel");
                Console.WriteLine("3. Checkout");
                Console.WriteLine("4: Vis spil");
                Console.WriteLine("5: Vis forespørgsler");
                Console.WriteLine("0: Afslutte program");
                Console.Write("Indtast valg: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("--- Opret Brætspil ---");
                        GameCreation();
                        Console.Write("Tryk en vilkårlig tast for at vende tilbage til menuen...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("--- Opret Forespørgsel ---");
                        CreateRequest();
                        Console.Write("Tryk en vilkårlig tast for at vende tilbage til menuen...");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("--- Check Brætspil ud ---");
                        Game.Checkout();
                        Console.Write("Tryk en vilkårlig tast for at vende tilbage til menuen...");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();
                        Console.Write("Vil du sortere spilene? (1. Ja / 2. Nej): ");
                        string sortInput = Console.ReadLine();
                        if (sortInput == "1")
                        {
                            Game.SortGames();
                        }
                        Console.Clear();
                        Console.WriteLine("--- Brætspil På Lager ---");
                        Game.DisplayGame();
                        //Game.DisplayGames();
                        Console.Write("Tryk en vilkårlig tast for at vende tilbage til menuen...");
                        Console.ReadKey();
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("--- Forespørgsler ---");
                        Request.DisplayRequests();
                        Console.WriteLine("Tryk en vilkårlig tast for at vende tilbage til menuen...");
                        Console.ReadKey();
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

            GameType existingGameType = PseudoDatabase.gametypes.FirstOrDefault(gt => gt.Name.Equals(gameName, StringComparison.OrdinalIgnoreCase));
            GameType gameTypeToUse;

            if (existingGameType != null)
            {
                Console.WriteLine($"Brætspillet {existingGameType.Name} findes allerede og bruges som skabelon.");
                gameTypeToUse = existingGameType;
            } else
            {
                Console.WriteLine($"Brætspillet {gameName} findes ikke, indtast venligst yderligere information for at oprette den.");

                Console.Write("Indtast beskrivelse: ");
                string gameDesc = Console.ReadLine();

                Console.Write("Indtast minimum alder: ");
                int minAge = int.Parse(Console.ReadLine());

                Console.Write("Indtast minimum spillere: ");
                int minPlayers = int.Parse(Console.ReadLine());

                Console.Write("Indtast maksimum antal spillere: ");
                int maxPlayers = int.Parse(Console.ReadLine());

                //Tjekker om input er i Enum, hvis ikke sættes den til default NA
                Console.Write("Indtast genre (f.eks. Campaign eller Familygame): ");
                string genreInput = Console.ReadLine();
                Genre genre = Enum.TryParse(genreInput, out Genre parsedGenre) ? parsedGenre : Genre.NA;

                //Opretter en ny GameType og tilføjer den til PseudoDatabase
                gameTypeToUse = new GameType(gameName, gameDesc, minAge, minPlayers, maxPlayers, genre);
                PseudoDatabase.gametypes.Add(gameTypeToUse);
            }

            //Spørg om pris og stand, ligemeget om GameType eksistere eller ej
            Console.Write("Indtast pris: ");
            double price = double.Parse(Console.ReadLine());

            //Tjekker om input er i Enum, hvis ikke sættes den til default NA
            Console.Write("Indtast spiltilstand (f.eks. Fine eller Perfect): ");
            string conditionInput = Console.ReadLine();
            Condition condition = Enum.TryParse(conditionInput, out Condition parsedCondition) ? parsedCondition : Condition.NA;

            //Opretter og tilføjer det nye spil og anvender GameType som blev bestemt før
            Game newGame = new Game(PseudoDatabase.games.Count + 1, price, gameTypeToUse, condition);
            PseudoDatabase.games.Add(newGame);

            Console.WriteLine($"Spillet '{gameTypeToUse.Name}' er oprettet med succes og tilføjet til listen over spil!");
        }


        static void CreateRequest()
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
            Console.WriteLine($"\nForespørgsel oprettet: {newRequest}");
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
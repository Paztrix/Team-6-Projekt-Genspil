using Genspil;
using System.Linq;

namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<GameDescription> gameDescriptions = DataHandler.LoadGameDescriptions();
            List<Game> games = DataHandler.LoadGames(gameDescriptions);
            List<Request> requests = DataHandler.LoadRequests();

            ShowMenu(games, gameDescriptions, requests);
        }

        public static void ShowMenu(List<Game> games, List<GameDescription> gameDescriptions, List<Request> requests)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Genspil Lager System ---");
                Console.WriteLine("1: Opret brætspil");
                Console.WriteLine("2. Checkout af brætspil");
                Console.WriteLine("3: Opret forespørgsel");
                Console.WriteLine("4: Fjern forespørgsel");
                Console.WriteLine("5: Vis spil");
                Console.WriteLine("6: Vis forespørgsler");
                Console.WriteLine("0: Afslutte program");
                Console.Write("Indtast valg: ");

                string input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "1":
                        MenuTitle("Opret brætspil");
                        Game newGame = GameCreation(gameDescriptions, games, requests);
                        games.Add(newGame);
                        DataHandler.SaveGames(games);
                        PressAnyKey();
                        break;

                    case "2":
                        MenuTitle("Checkout af brætspil");
                        CheckoutGame(games);
                        PressAnyKey();
                        break;

                    case "3":
                        MenuTitle("Opret forespørgsel");
                        Request newRequest = CreateRequest(requests);
                        requests.Add(newRequest);
                        DataHandler.SaveRequests(requests);
                        PressAnyKey();
                        break;

                    case "4":
                        MenuTitle("Fjern forespørgsel");
                        RemoveRequest(requests);
                        PressAnyKey();
                        break;

                    case "5":
                        MenuTitle("Brætspil på lager");
                        Game.DisplayGames(games);
                        PressAnyKey();
                        break;

                    case "6":
                        MenuTitle("Forespørgsler");
                        Request.DisplayRequests(requests);
                        PressAnyKey();
                        break;

                    case "0":
                        Console.WriteLine("Afslutter programmet");
                        return;

                    default:
                        Console.WriteLine("Ugyldigt valg");
                        PressAnyKey();
                        break;
                }
            }
        }

        public static Game GameCreation(List<GameDescription> gameDescriptions, List<Game> games, List<Request> requests)
        {
            Console.Write("Navn på spil: ");
            string gameName = Console.ReadLine();

            var matchingRequests = requests
                .Where(r => r.RequestedGame.Equals(gameName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matchingRequests.Any())
            {
                Console.WriteLine($"{gameName} har {matchingRequests.Count} {(matchingRequests.Count == 1 ? "forespørgsel" : "forespørgsler")}.");
            }

            GameDescription existingGameType = gameDescriptions
                .FirstOrDefault(gt => gt.Name.Equals(gameName, StringComparison.OrdinalIgnoreCase));
            GameDescription gameTypeToUse;

            if (existingGameType != null)
            {
                gameTypeToUse = existingGameType;
            }
            else
            {
                Console.Write("Beskrivelse: ");
                string gameDesc = Console.ReadLine();
                Console.Write("Min. alder: ");
                int minAge = int.Parse(Console.ReadLine());
                Console.Write("Min. spillere: ");
                int minPlayers = int.Parse(Console.ReadLine());
                Console.Write("Max. spillere: ");
                int maxPlayers = int.Parse(Console.ReadLine());
                Console.Write("Genre: ");
                string genreInput = Console.ReadLine();
                Genre genre = Enum.TryParse(genreInput, out Genre parsedGenre) ? parsedGenre : Genre.NA;

                // Opretter en ny GameDescription og tilføjer den til listen
                gameTypeToUse = new GameDescription(gameName, gameDesc, minAge, minPlayers, maxPlayers, genre);
                gameDescriptions.Add(gameTypeToUse);
                DataHandler.SaveGameDescriptions(gameDescriptions);
            }

            // Spørg om pris og stand, ligemeget om GameDescription eksisterer eller ej
            Console.Write("Indtast pris: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Indtast stand (Perfect, Fine, InWorkshop, Unplayable): ");
            string conditionInput = Console.ReadLine();
            Condition condition = Enum.Parse<Condition>(conditionInput);

            int nextId = games.Count > 0 ? games.Max(g => g.Id) + 1 : 1;

            return new Game(nextId, price, gameTypeToUse, condition);
        }

        public static void CheckoutGame(List<Game> games)
        {
            Console.Write("Søg efter spilnavn: ");
            string search = Console.ReadLine()?.Trim().ToLower();

            var matchingGames = games
                .Where(g => g.type.Name.ToLower().Contains(search))
                .ToList();

            if (matchingGames.Count == 0)
            {
                Console.WriteLine("Ingen spil matcher søgningen.");
                return;
            }

            Console.WriteLine("\nFundne spil:");
            Console.WriteLine($"{"Nr.",-5} {"Navn",-20} {"Pris",-10} {"Stand",-12} {"ID",-5}");
            Console.WriteLine(new string('-', 55));

            for (int i = 0; i < matchingGames.Count; i++)
            {
                var g = matchingGames[i];
                Console.WriteLine($"{i,-5} {g.type.Name,-20} {g.price,-10:F2} {g.GameCondition,-12} {g.Id,-5}");
            }

            Console.Write("\nIndtast nummer på spil du vil checkout: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < matchingGames.Count)
            {
                Game selectedGame = matchingGames[index];
                games.Remove(selectedGame);
                DataHandler.SaveGames(games);
                Console.WriteLine($"\n'{selectedGame.type.Name}' er nu fjernet fra lageret.");
            }
            else
            {
                Console.WriteLine("Ugyldigt valg.");
            }
        }

        public static Request CreateRequest(List<Request> requests)
        {
            Console.Write("Navn: ");
            string name = Console.ReadLine();

            Console.Write("Telefonnummer: ");
            string phone = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Ønsket spil: ");
            string requestedGame = Console.ReadLine();

            int newId = requests.Count > 0 ? requests.Max(r => r.ID) + 1 : 1;
            DateTime createdOn = DateTime.Now;

            return new Request(newId, createdOn, name, phone, email, requestedGame);
        }

        public static void RemoveRequest(List<Request> requests)
        {
            Console.Write("Søg efter navn: ");
            string search = Console.ReadLine()?.Trim().ToLower();

            var matchingRequests = requests
                .Where(r => r.Name.ToLower().Contains(search))
                .ToList();

            if (matchingRequests.Count == 0)
            {
                Console.WriteLine("Ingen forespørgsler matcher søgningen.");
                return;
            }

            Console.WriteLine("\nFundne forespørgsler:");
            Console.WriteLine($"{"Nr.",-5} {"Navn",-20} {"Email",-25} {"Spil",-20}");
            Console.WriteLine(new string('-', 75));

            for (int i = 0; i < matchingRequests.Count; i++)
            {
                var r = matchingRequests[i];
                Console.WriteLine($"{i,-5} {r.Name,-20} {r.Email,-25} {r.RequestedGame,-20}");
            }

            Console.Write("\nIndtast nummer på forespørgsel du vil fjerne: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < matchingRequests.Count)
            {
                var removed = matchingRequests[index];
                requests.Remove(removed);
                DataHandler.SaveRequests(requests);
                Console.WriteLine($"\nForespørgslen fra '{removed.Name}' om spillet '{removed.RequestedGame}' er nu fjernet.");
            }
            else
            {
                Console.WriteLine("Ugyldigt valg.");
            }
        }

        public static void MenuTitle(string title)
        {
            Console.Clear();
            Console.WriteLine("---- " + title + " ----");
        }

        public static void PressAnyKey()
        {
            Console.WriteLine("\nTryk en vilkårlig tast for at fortsætte...");
            Console.ReadKey();
        }
    }
}


/* Pseudokode til at oprette et nyt spil
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

    //Opret ny instans af GameDescription med de indtastede oplysninger
    newGame = new GameDescription(gameName, gameDesc, minAge, minPlayers, maxPlayers)

    //bekræft spillet er oprettet
    Display ( Spillet {gameName} er oprettet)

    return newGame
*/



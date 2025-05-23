using Genspil;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;

namespace Genspil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<GameType> gameDescriptions = DataHandler.LoadGameTypes();
            List<Game> games = DataHandler.LoadGames(gameDescriptions);
            List<Request> requests = DataHandler.LoadRequests();

            ShowMenu(games, gameDescriptions, requests);
        }

        public static void ShowMenu(List<Game> games, List<GameType> gameDescriptions, List<Request> requests)
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
                Console.WriteLine("7: Søg efter spil");
                Console.WriteLine("8. Lagerliste");
                Console.WriteLine("0: Afslut program");
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
                        Game.SortGames(games);
                        PressAnyKey();
                        break;

                    case "6":
                        MenuTitle("Forespørgsler");
                        Request.DisplayRequests(requests);
                        PressAnyKey();
                        break;

                    case "7":
                        MenuTitle("Søg efter spil");
                        SearchGame(games);
                        PressAnyKey();
                        break;

                    case "8":
                        MenuTitle("Lagerliste");
                        GameType.DisplayGameDescription(gameDescriptions);
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

        //Mangler exception handling
        public static Game GameCreation(List<GameType> gameDescriptions, List<Game> games, List<Request> requests)
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

            GameType existingGameType = gameDescriptions
                .FirstOrDefault(gt => gt.Name.Equals(gameName, StringComparison.OrdinalIgnoreCase));
            GameType gameTypeToUse;

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
                Console.WriteLine("Familie, Kampagne, Puzzle, Strategi, Børn eller Quiz");
                Console.Write("Genre: ");
                string genreInput = Console.ReadLine();
                Genre genre = Enum.TryParse(genreInput, out Genre parsedGenre) ? parsedGenre : Genre.NA;

                // Opretter en ny GameType og tilføjer den til listen
                gameTypeToUse = new GameType(gameName, gameDesc, minAge, minPlayers, maxPlayers, genre);
                gameDescriptions.Add(gameTypeToUse);
                DataHandler.SameGameTypes(gameDescriptions);
            }

            // Spørg om pris og stand, ligemeget om GameType eksisterer eller ej
            Console.Write("Indtast pris: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Indtast stand (Perfekt, Brugsspor, Slidt, Reperation): ");
            string conditionInput = Console.ReadLine();
            Condition condition = Enum.TryParse(conditionInput, out Condition parsedCondition) ? parsedCondition : Condition.NA;

            int nextId = games.Count > 0 ? games.Max(g => g.Id) + 1 : 1;

            Console.WriteLine($"Spillet '{gameTypeToUse.Name}', er tilføjet til listen over spil!");

            return new Game(nextId, price, gameTypeToUse, condition);
        }

        //Metode til at fjerne spil fra listen
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
            Console.WriteLine($"{"Nr.",-5} {"Navn",-20} {"Pris",-10} {"Stand",-12}");
            Console.WriteLine(new string('-', 55));

            for (int i = 0; i < matchingGames.Count; i++)
            {
                var g = matchingGames[i];
                Console.WriteLine($"{i,-5} {g.type.Name,-20} {g.price,-10:F2} {g.GameCondition,-12}");
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

        //Opretter en forespørgsel
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

        //Fjerner en forespørgsel
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

        //Søge efter specifikt spil ved navn
        public static void SearchGame(List<Game> games)
        {
            Console.Write("Indtast navn eller del af navnet på spil: ");
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
            Console.WriteLine($"{"Navn",-25} {"Pris",-10} {"Stand",-15}");
            Console.WriteLine(new string('-', 60));

            foreach (var game in matchingGames)
            {
                Console.WriteLine($"{game.type.Name,-25} {game.price,-10:F2} {game.GameCondition,-15}");
            }
        }
        public static void MenuTitle(string title)
        {
            Console.Clear();
            Console.WriteLine("---- " + title + " ----");
        }

        public static void PressAnyKey()
        {
            Console.Write("\nTryk en vilkårlig tast for at fortsætte...");
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

    //Opret ny instans af GameType med de indtastede oplysninger
    newGame = new GameType(gameName, gameDesc, minAge, minPlayers, maxPlayers)

    //bekræft spillet er oprettet
    Display ( Spillet {gameName} er oprettet)

    return newGame
*/
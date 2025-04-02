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

            Console.Write("Press key to stop program...");
            Console.ReadLine();

class GameCreation
       {
           static void Main(string[] args)
           {
               GameType newGame = CreateNewGame();
               Console.WriteLine($"Spillet {newGame.GameName} er oprettet!");
           }

           static GameType CreateNewGame()
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

               
               Console.WriteLine($"Spillet {gameName} er oprettet.");

               return newGame;
           }
       }

       class GameCreation
       {
           public string GameName { get; set; }
           public string GameDescription { get; set; }
           public int MinAge { get; set; }
           public int MinPlayers { get; set; }
           public int MaxPlayers { get; set; }

           public GameCreation(string gameName, string gameDescription, int minAge, int minPlayers, int maxPlayers)
           {
               GameName = gameName;
               GameDescription = gameDescription;
               MinAge = minAge;
               MinPlayers = minPlayers;
               MaxPlayers = maxPlayers;
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

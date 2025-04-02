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

            Console.WriteLine("Press key to stop program");
            Console.ReadLine();
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
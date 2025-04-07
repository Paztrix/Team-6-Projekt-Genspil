using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genspil
{
    public class Request
    {
        //Private variable for Request-klassen
        private int _id;
        private DateTime _createdOn;
        private string _name;
        private string _phone;
        private string _email;
        private string _requestedGame;

        //get set metoder for private variable til public
        public int ID { get { return _id; } set { _id = value; } }
        public DateTime CreatedOn { get { return _createdOn; } set { _createdOn = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string RequestedGame { get { return _requestedGame; } set { _requestedGame = value; } }

        //Konstruktør der tager Request-klasse variable som parametre
        public Request(int id, DateTime createdOn, string name, string phone, string email, string requestedGame)
        {
            ID = id;
            CreatedOn = createdOn;
            Name = name;
            Phone = phone;
            Email = email;
            RequestedGame = requestedGame;
        }
        public override string ToString()
        {
            return $"{ID};{CreatedOn};{Name};{Phone};{Email};{RequestedGame}";
        }

        public static Request FromString(string line)
        {
            var parts = line.Split(';');
            return new Request(
                int.Parse(parts[0]),                     // ID
                DateTime.Parse(parts[1]),                // CreatedOn
                parts[2],                                // Name
                parts[3],                                // Phone
                parts[4],                                // Email
                parts[5]                                 // RequestedGame
            );
        }

        //Display metode der printer request
        public static void DisplayRequests(List<Request> requests)
        {
            // Definere hver kolonnes bredde: Brætspil | Kundenavn | Telefon | Email |
            int col1Width = 12, col2Width = 17, col3Width = 10, col4Width = 25;
            string separator = new string('-', col1Width + col2Width + col3Width + col4Width + 13);

            // Printer en header
            Console.WriteLine(separator);
            Console.WriteLine($"| {"Brætspil".PadRight(col1Width)} | {"Kundenavn".PadRight(col2Width)} | {"Telefon".PadRight(col3Width)} | {"Email".PadRight(col4Width)} |");
            Console.WriteLine(separator);

            // Itererer gennem alle forespørgsler og printer
            foreach (var request in requests)
            {
                Console.WriteLine($"| {request.RequestedGame.PadRight(col1Width)} | {request.Name.PadRight(col2Width)} | {request.Phone.PadRight(col3Width)} | {request.Email.PadRight(col4Width)} |");
                Console.WriteLine(separator);
            }
        }
    }
}

/*
|--------------------------------------------------------------|
| Brætspil | Kundenavn     | Telefon  | Email                  |
|--------------------------------------------------------------|
| Monopoly | John Does     | 12345678 | johndoe@email.com      |
|--------------------------------------------------------------|
| Jumanji  | Jane Smith    | 98765432 | janesmith@email.com    |
|--------------------------------------------------------------|
| Ludo     | Alice Johnson | 55512345 | alicejohnson@email.com |
|--------------------------------------------------------------|
*/
using System;
using System.Collections.Generic;
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
        private GameType _gameType;

        //get set metoder for private variable til public
        public int ID { get { return _id; } set { _id = value; } }
        public DateTime CreatedOn { get { return _createdOn; } set { _createdOn = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Phone { get { return _phone; } set { _phone = value; } }
        public string Email { get { return _email; } set { _email = value; } }

        //Konstruktør der tager Request-klasse variable som parametre
        public Request(int id, DateTime createdOn, string name, string phone, string email, GameType gameType)
        {
            ID = id;
            CreatedOn = createdOn;
            Name = name;
            Phone = phone;
            Email = email;
            this._gameType = gameType;
        }

        //Display metode der printer alle requests
        public static void DisplayRequests()
        {
            foreach (var request in PsudoDatabase.requests)
            {
                Console.WriteLine($"ID: {request.ID}, Name: {request.Name}, Phone: {request.Phone}, Email: {request.Email}, Created On: {request.CreatedOn}, Requested {request._gameType.Name}");
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace Genspil
{
    public class Game
    {
        private int id;
        public double price { get; private set; }
        public Condition GameCondition { get; private set; }
        public GameType type { get; private set; }

        public Game(int id, double price, GameType gametype, Condition condition = Condition.Perfect)
        {
            this.id = id;
            this.price = price;
            this.GameCondition = condition;
            this.type = gametype;
        }

        public void Display() { }

        public void Checkout() { }
    }
}

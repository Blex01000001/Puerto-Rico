using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    abstract class Role
    {
        public string Name { get; set; }
        public int Money { get; set; }
        protected Role() { }

        abstract public void action(Game game);
    }

    internal class Settler : Role //開拓者
    {
        public Settler()
        {
            Name = "Settler";
        }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
            if (game.quarryFields.Count != 0)
            {
                Console.WriteLine($"{game.players[0].Name} select the QuarryField");
                game.players[0].FarmList.Add(game.quarryFields[0]);
                game.quarryFields.RemoveAt(0);
            }
            else
            {
                Console.WriteLine($"{game.players[0].Name} select the {game.availableFarms[0].Name}");
                game.players[0].FarmList.Add(game.availableFarms[0]);
                game.availableFarms.RemoveAt(0);
            }
            for (int i = 1; i < game.players.Count; i++)
            {
                Console.WriteLine($"{game.players[i].Name} select the {game.availableFarms[0].Name}");
                game.players[i].FarmList.Add(game.availableFarms[0]);
                game.availableFarms.RemoveAt(0);
            }
            game.availableFarms.Clear();
            game.availableFarms = game.HideFarms.Take(game.playerNum + 1).ToList();

        }
    }
    internal class Mayor : Role
    {
        public Mayor()
        {
            Name = "Mayor";
        }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Builder : Role
    {
        public Builder() { Name = "Builder"; }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Craftsman : Role
    {
        public Craftsman() { Name = "Craftsman"; }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Trader : Role
    {
        public Trader() { Name = "Trader"; }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Captain : Role
    {
        public Captain() { Name = "Captain"; }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Prospector1 : Role
    {
        public Prospector1() { Name = "Prospector_1"; }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Prospector2 : Role
    {
        public Prospector2() { Name = "Prospector_2"; }
        public override void action(Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }





}

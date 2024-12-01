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

        abstract public void action(Player player, Game game);
    }

    internal class Settler : Role //開拓者
    {
        public Settler()
        {
            Name = "Settler";
        }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"  {Name} Action");
            if (game.quarryFields.Count != 0)
            {
                Console.WriteLine($"  {player.Name} select the QuarryField");
                player.FarmList.Add(game.quarryFields[0]);
                game.quarryFields.RemoveAt(0);
            }
            else
            {
                Console.WriteLine($"  {player.Name} select the {game.availableFarms[0].Name}");
                player.FarmList.Add(game.availableFarms[0]);
                game.availableFarms.RemoveAt(0);
            }

            int gamePlayerCount = game.players.Count;
            int playerIndex = game.players.FindIndex(x => x == player);

            for (int i = 1; i < gamePlayerCount; i++)
            {
                int ii = i + playerIndex > gamePlayerCount - 1 ? i + playerIndex - gamePlayerCount : i + playerIndex;
                game.players[ii].FarmList.Add(game.availableFarms[0]);
                Console.WriteLine($"  {game.players[ii].Name} select the {game.availableFarms[0].Name}");
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
            Name = "Mayor  ";
        }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
            player.Worker += 1;
        }
    }
    internal class Builder : Role
    {
        public Builder() { Name = "Builder"; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Craftsman : Role
    {
        public Craftsman() { Name = "Craftsman"; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Trader : Role
    {
        public Trader() { Name = "Trader "; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Captain : Role
    {
        public Captain() { Name = "Captain"; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Prospector1 : Role
    {
        public Prospector1() { Name = "Prospecto1"; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }
    internal class Prospector2 : Role
    {
        public Prospector2() { Name = "Prospecto2"; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
        }
    }





}

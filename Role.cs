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
            if (game.quarryFields.Count > 0 && player.FarmList.Count < 12)//如果礦場不為零的話就拿礦場
            {
                Console.WriteLine($"  {player.Name} select the QuarryField");
                player.FarmList.Add(game.quarryFields[0]);
                game.quarryFields.Remove(game.quarryFields[0]);
            }
            else if(player.FarmList.Count < 12 && game.availableFarms.Count > 0)
            {
                Console.WriteLine($"  {player.Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
                player.FarmList.Add(game.availableFarms[0]);
                game.availableFarms.Remove(game.availableFarms[0]);
            }
            else
            {
                Console.WriteLine($"{player.Name} field is full");
            }

            int gamePlayerCount = game.players.Count;
            int playerIndex = game.players.FindIndex(x => x == player);

            for (int i = 1; i < gamePlayerCount; i++)//其他人輪流拿農田
            {
                if(game.availableFarms.Count <= 0)
                {
                    Console.WriteLine("No available Farms");
                    continue;
                }
                int ii = i + playerIndex > gamePlayerCount - 1 ? i + playerIndex - gamePlayerCount : i + playerIndex;
                if (game.players[ii].FarmList.Count >= 12)
                {
                    Console.WriteLine($"{game.players[ii].Name} field is full");
                    continue;
                }
                Console.WriteLine($"  {game.players[ii].Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
                game.players[ii].FarmList.Add(game.availableFarms[0]);
                game.availableFarms.Remove(game.availableFarms[0]);
            }

            game.ArrangeFarms();

        }
    }
    internal class Mayor : Role //市長
    {
        public Mayor()
        {
            Name = "Mayor  ";
        }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
            Console.WriteLine($" {player.Name} get 1 worker(Mayor)");
            game.bank.getWorkerFromBank(player);//市長特權
            int playerIndex = game.players.FindIndex(x => x == player);

            for (int i = 0; i < game.bank.WorkerShip; i++)//從市長開始每個人輪流拿工人，拿到移民船上沒有工人
            {
                int ii = (i % game.playerNum) + playerIndex > (game.playerNum - 1) ? (i % game.playerNum) + playerIndex - game.playerNum : (i % game.playerNum) + playerIndex;
                //int ii = (i % game.playerNum) > (game.playerNum - 1) ? (i % game.playerNum) + i - game.playerNum : (i % game.playerNum) + i;
                game.players[ii].Worker += 1;
                Console.WriteLine($" {game.players[ii].Name} get 1 worker");
            }
            game.bank.WorkerShip = 0;
            int totalPlayerEmptyBuilding = 0;
            //game.showAvailableFarms();
            //game.showPlayerStatus();
            foreach (Player p1 in game.players)//所有人必須將得到的移民放在地圖上任何有圈圈的地方（包括農田方塊或者建築物上），而之前部署的任何移民，可以在此時重新部署（但仍然只要有空圈圈且有空間的移民就要部署）
            {
                //Console.WriteLine($"***{p1.Name}: TURN***");
                List<Building> BuildingList = p1.getBuildingList();
                //Console.WriteLine($"***BuildingList.count: {BuildingList.Count}");
                p1.clearFarmWorker();
                p1.clearFactoryWorker();

                for (int i = 0; i < p1.Worker; i++)
                {
                    //Console.WriteLine($"***{p1.Name}: TURN-2***");
                    if (BuildingList.Count <= 0)
                        break;
                    //Console.WriteLine($"***{p1.Name}: TURN-3***");
                    BuildingList[0].worker += 1;
                    Console.WriteLine($"  {p1.Name} put 1 worker on {BuildingList[0].Name}({BuildingList[0].GetHashCode()})");
                    BuildingList.RemoveAt(0);
                }
                totalPlayerEmptyBuilding += BuildingList.Count;
            }
            game.bank.WorkerShip = totalPlayerEmptyBuilding > game.playerNum ? totalPlayerEmptyBuilding : game.playerNum;
            game.bank.Worker -= game.bank.WorkerShip;
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

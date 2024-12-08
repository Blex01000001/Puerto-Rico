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
                                  //選擇開拓者的玩家可以不拿郊區方塊嗎?
                                  //其他玩家可以不拿郊區方塊嗎?
    {
        public Settler()
        {
            Name = "Settler";
            Money = 0;
        }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"  {Name} Action");
            if (game.quarryFields.Count > 0 && player.FarmList.Count < 12)//如果礦場不為零的話就拿礦場
            {
                Console.WriteLine($"  {player.Name} select the {game.quarryFields[0].Name}({game.quarryFields[0].GetHashCode()}");
                player.FarmList.Add(game.quarryFields[0]);
                //player.BuildingList.Add(game.quarryFields[0]);
                game.quarryFields.Remove(game.quarryFields[0]);
            }
            else if(player.FarmList.Count < 12 && game.availableFarms.Count > 0)
            {
                Console.WriteLine($"  {player.Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
                player.FarmList.Add(game.availableFarms[0]);
                //player.BuildingList.Add(game.availableFarms[0]);
                game.availableFarms.Remove(game.availableFarms[0]);
            }
            else
            {
                Console.WriteLine($"{player.Name} field is full");
            }

            int gamePlayerCount = Player.list.Count;
            int playerIndex = Player.list.FindIndex(x => x == player);

            for (int i = 1; i < gamePlayerCount; i++)//其他人輪流拿農田
            {
                if(game.availableFarms.Count <= 0)
                {
                    Console.WriteLine("No available Farms");
                    continue;
                }
                int ii = i + playerIndex > gamePlayerCount - 1 ? i + playerIndex - gamePlayerCount : i + playerIndex;
                if (Player.list[ii].FarmList.Count >= 12)
                {
                    Console.WriteLine($"{Player.list[ii].Name} field is full");
                    continue;
                }
                Console.WriteLine($"  {Player.list[ii].Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
                Player.list[ii].FarmList.Add(game.availableFarms[0]);
                //Player.list[ii].BuildingList.Add(game.availableFarms[0]);
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
            Money = 0;
        }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
            Console.WriteLine($" {player.Name} get 1 worker(Mayor)");
            game.bank.getWorkerFromBank(player);//市長特權

            int playerIndex = Player.list.FindIndex(x => x == player);
            for (int i = 0; i < game.bank.WorkerShip; i++)//從市長開始每個人輪流拿工人，拿到移民船上沒有工人
            {
                int ii = (i % game.playerNum) + playerIndex > (game.playerNum - 1) ? (i % game.playerNum) + playerIndex - game.playerNum : (i % game.playerNum) + playerIndex;
                Player.list[ii].Worker += 1;
                Console.WriteLine($" {Player.list[ii].Name} get 1 worker");
            }
            game.bank.WorkerShip = 0;
            int totalPlayerEmptyCircle = 0;
            foreach (Player p1 in Player.list)//所有人必須將得到的移民放在地圖上任何有圈圈的地方（包括農田方塊或者建築物上），而之前部署的任何移民，可以在此時重新部署（但仍然只要有空圈圈且有空間的移民就要部署）
            {
                List<Building> EmptyCircleList = p1.getEmptyCircleList();
                p1.clearFarmWorker();
                p1.clearFactoryWorker();

                for (int i = 0; i < p1.Worker; i++)
                {
                    if (EmptyCircleList.Count <= 0)
                        break;
                    EmptyCircleList[0].worker += 1;
                    Console.WriteLine($"  {p1.Name} put 1 worker on {EmptyCircleList[0].Name}({EmptyCircleList[0].GetHashCode()})");
                    EmptyCircleList.RemoveAt(0);
                }
                totalPlayerEmptyCircle += EmptyCircleList.Count;
            }
            game.bank.WorkerShip = totalPlayerEmptyCircle > game.playerNum ? totalPlayerEmptyCircle : game.playerNum;
            game.bank.Worker -= game.bank.WorkerShip;
            if (game.bank.Worker < 0)//移民不夠補充移民船時，則遊戲結束事件發生
            {
                Console.WriteLine("\n>>>>移民不夠補充移民船，遊戲將在角色輪轉後結束<<<<\n");
                game.EndGame = true;
            }
        }
    }
    internal class Builder : Role //建築師
        //建築師可以不建嗎?
        //其他玩家可以不建嗎?
    {
        public Builder() { Name = "Builder"; Money = 0; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($"{Name} Action");
            int playerIndex = Player.list.FindIndex(x => x == player);
            for (int i = 0; i < Player.list.Count; i++)//由建築師本身開始，可以依照上下家順序興建一座建築物。建築師可以在興建任何建築物的時候少花一元（最少可以免費；特權）。
            {
                int ii = (i % game.playerNum) + playerIndex > (game.playerNum - 1) ? (i % game.playerNum) + playerIndex - game.playerNum : (i % game.playerNum) + playerIndex;
                //判斷是否可以從availableBuildings買建築，可以就直接買
                for (int j = 0; j < game.availableBuildings.Count; j++)//逐一檢查每個建築
                {
                    if (game.availableBuildings[j].Name == "PassBuilding")//選到PassBuilding代表該玩家pass掉買建築物
                    {
                        Console.WriteLine($" {Player.list[ii].Name} does not buy the building");
                        break;
                    }
                    int buildingCost = Func.checkDis(Player.list[ii], game.availableBuildings[j]);
                    if (buildingCost < 0) //買不起就換下一個建築
                        continue;
                    //買得起就直接買
                    Player.list[ii].BuildingList.Add(game.availableBuildings[j]);
                    game.availableBuildings.Remove(game.availableBuildings[j]);
                    //扣錢還給銀行
                    Player.list[ii].Money -= buildingCost;
                    game.bank.addMoney(buildingCost);
                    break;
                }
            }
        }
    }
    internal class Craftsman : Role //工匠
        //工匠特權所獲得的那一個資源是否要等全部人都收成完才能拿? 還是在工匠收成時就可以直接拿?
    {
        public Craftsman() { Name = "Craftsman"; Money = 0; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
            int playerIndex = Player.list.FindIndex(x => x == player);
            for (int i = 0; i < Player.list.Count; i++)//所有人從工匠開始，依照上下家次序依序獲得收成。若商品資源已經耗盡，之後的人就拿不到收成了
            {
                int ii = (i % game.playerNum) + playerIndex > (game.playerNum - 1) ? (i % game.playerNum) + playerIndex - game.playerNum : (i % game.playerNum) + playerIndex;
                foreach (Goods good in game.bank.GoodList)
                {
                    int checkedHarvest = Func.CheckedHarvest(Player.list[ii], good);
                    if (checkedHarvest <= 0)//小於0代表無法收成
                        continue;
                    int getGoodQTY = game.bank.getGoods(good, checkedHarvest);
                    Player.list[ii].Goods.Find(x => x.name == good.name).qty += getGoodQTY;
                    Console.WriteLine($" {Player.list[ii].Name} get {getGoodQTY} {good.name} from bank");
                }
            }
        }
    }
    internal class Trader : Role
    {
        public Trader() { Name = "Trader "; Money = 0; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
        }
    }
    internal class Captain : Role
    {
        public Captain() { Name = "Captain"; Money = 0; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
        }
    }
    internal class Prospector1 : Role //礦工1
    {
        public Prospector1() { Name = "Prospecto1"; Money = 0; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
            player.GetMoneyFromBank(1);
        }
    }
    internal class Prospector2 : Role //礦工1
    {
        public Prospector2() { Name = "Prospecto2"; Money = 0; }
        public override void action(Player player, Game game)
        {
            Console.WriteLine($" {Name} Action");
            player.GetMoneyFromBank(1);
        }
    }





}

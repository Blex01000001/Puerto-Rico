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

        abstract public void Action(Player player, Game game);
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
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            //開拓者選擇一個打開的郊區方塊，或者拿一個採礦場（特權），放在自己的郊區空格上；接著，下家可以從剩下打開的郊區方塊中選擇一個，放在自己的郊區空格上；依此類推。
            foreach (Player player1 in Player.GetPlayerListFromRole(player))
            {
                bool ignoreSalesRules = Func.CheckBuildingWithWorker(player1, typeof(Constructionhut));
                if(player1.Role == "Settler" && game.quarryFields.Count > 0 && player.FarmList.Count < 12)
                {

                }
            }






            if (game.quarryFields.Count > 0 && player.FarmList.Count < 12)//如果礦場不為零的話就拿礦場
            {
                Console.WriteLine($"\t{player.Name} select the {game.quarryFields[0].Name}({game.quarryFields[0].GetHashCode()}");
                player.FarmList.Add(game.quarryFields[0]);
                game.quarryFields.Remove(game.quarryFields[0]);
            }
            else if(player.FarmList.Count < 12 && game.availableFarms.Count > 0)
            {
                Console.WriteLine($"\t{player.Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
                player.FarmList.Add(game.availableFarms[0]);
                //player.BuildingList.Add(game.availableFarms[0]);
                game.availableFarms.Remove(game.availableFarms[0]);
            }
            else
            {
                Console.WriteLine($"\t{player.Name} field is full");
            }

            int gamePlayerCount = Player.list.Count;
            int playerIndex = Player.list.FindIndex(x => x == player);

            for (int i = 1; i < gamePlayerCount; i++)//其他人輪流拿農田
            {
                if(game.availableFarms.Count <= 0)
                {
                    Console.WriteLine("***No available Farms***");
                    continue;
                }
                int ii = i + playerIndex > gamePlayerCount - 1 ? i + playerIndex - gamePlayerCount : i + playerIndex;
                if (Player.list[ii].FarmList.Count >= 12)
                {
                    Console.WriteLine($"***{Player.list[ii].Name} field is full***");
                    continue;
                }
                Console.WriteLine($"\t{Player.list[ii].Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
                Player.list[ii].FarmList.Add(game.availableFarms[0]);
                //Player.list[ii].BuildingList.Add(game.availableFarms[0]);
                game.availableFarms.Remove(game.availableFarms[0]);
            }

            game.ArrangeFarms();

        }
    }
    internal class Mayor : Role //市長
    {//移民不夠補充移民船時，是此次腳色輪轉後結束還是下一回合才結束?
        public Mayor()
        {
            Name = "Mayor  ";
            Money = 0;
        }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            Console.WriteLine($"\t{player.Name} get 1 worker(Mayor)");
            game.bank.getWorkerFromBank(player);//市長特權
            int playerIndex = Player.list.FindIndex(x => x == player);
            foreach (Player player1 in Player.GetPlayerListFromRole(player))//從市長開始每個人輪流拿工人，拿到移民船上沒有工人
            {
                player1.Worker += 1;
                Console.WriteLine($"\t{player1.Name} get 1 worker");
            }
            game.bank.WorkerShip = 0;
            int totalPlayerEmptyCircle = 0;
            foreach (Player player2 in Player.GetPlayerListFromRole(player))//所有人必須將得到的移民放在地圖上任何有圈圈的地方（包括農田方塊或者建築物上），而之前部署的任何移民，可以在此時重新部署（但仍然只要有空圈圈且有空間的移民就要部署）
            {
                List<Building> EmptyCircleList = player2.GetEmptyCircleList();
                player2.ClearFarmWorker();
                player2.ClearFactoryWorker();
                for (int i = 0; i < player2.Worker; i++)
                {
                    if (EmptyCircleList.Count <= 0)
                        break;
                    EmptyCircleList[0].worker += 1;
                    Console.WriteLine($"\t\t{player2.Name} put 1 worker on {EmptyCircleList[0].Name}({EmptyCircleList[0].GetHashCode()})");
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
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            int playerIndex = Player.list.FindIndex(x => x == player);
            foreach (Player player1 in Player.GetPlayerListFromRole(player))//由建築師本身開始，可以依照上下家順序興建一座建築物。建築師可以在興建任何建築物的時候少花一元（最少可以免費；特權）。
            {
                for (int j = 0; j < game.availableBuildings.Count; j++)//逐一檢查每個建築
                {
                    if (game.availableBuildings[j].Name == "PassBuilding")//選到PassBuilding代表該玩家pass掉買建築物
                    {

                        Console.WriteLine($"\t\t{player1.Name} PASS buy the building");
                        break;
                    }
                    int containBuildingIndex = player1.GetAllBuildings().FindIndex(x => x.Name == game.availableBuildings[j].Name);
                    if (containBuildingIndex >= 0)//檢查玩家有沒有重複的建築
                        continue;
                    int buildingCost = Func.checkDis(player1, game.availableBuildings[j]);
                    if (buildingCost < 0) //買不起就換下一個建築
                        continue;
                    //買得起就直接買
                    Console.WriteLine($"\t\t{player1.Name} cost {buildingCost} buy the {game.availableBuildings[j].Name}");
                    player1.BuildingList.Add(game.availableBuildings[j]);
                    game.availableBuildings.Remove(game.availableBuildings[j]);
                    //扣錢還給銀行
                    player1.Money -= buildingCost;
                    game.bank.addMoney(buildingCost);
                    break;
                }
                game.RndAvailableBuildings();
            }
        }
    }
    internal class Craftsman : Role //工匠
    //工匠特權所獲得的那一個資源是否要等全部人都收成完才能拿? 還是在工匠收成時就可以直接拿?
    {
        public Craftsman() { Name = "Craftsman"; Money = 0; }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            foreach (Player player1 in Player.GetPlayerListFromRole(player))
            {
                foreach (Goods good in game.bank.GoodList)
                {
                    int checkedHarvest = Func.CheckedHarvest(player1, good);
                    if (checkedHarvest <= 0)//小於0代表無法收成
                        continue;
                    int getGoodQTY = game.bank.getGoods(good, checkedHarvest);
                    player1.Goods.Find(x => x.name == good.name).qty += getGoodQTY;
                    Console.WriteLine($"\t\t{player1.Name} get {getGoodQTY} {good.name} from bank");
                }
            }
        }
    }
    internal class Trader : Role //商人
     //所有人從商人開始，依照上下家次序，可以選擇將手中的一個商品放進商店，以獲得金錢
     //販賣的動作只能進行一圈嗎?
    {
        public Trader() { Name = "Trader "; Money = 0; }
        public override void Action(Player player, Game game)
        {
            //Console.WriteLine($"Player COUNT: {Player.GetPlayerListFromRole(player).Count}");
            Console.WriteLine($"\t{Name} Action");
            foreach (Player player1 in Player.GetPlayerListFromRole(player))
            {
                //商店只有四格商品的空間。在任何一圈當中，只要商店的空間滿了，任何人在該圈將無法販賣商品
                if (game.ShopGoods.Count >= 4)//商店滿了不能賣
                {
                    Console.WriteLine("***商店已滿，無法販售***");
                    continue;
                }
                List<Goods> tempGoodList = player1.Goods.OrderBy(x => Func.RndNum()).ToList();
                foreach (Goods good in tempGoodList)
                {//辦公室作用時，當商人出現時，玩家可以不受商店不收不同種類商品的限制，販賣商店內已經有的商品給商店。但玩家仍然只能每次賣一個商品
                    bool ignoreSalesRules = Func.CheckBuildingWithWorker(player1, typeof(Office));
                    int playerGoodQTY = player1.Goods.Find(x => x.name == good.name).qty;
                    int containGoodIndex = game.ShopGoods.FindIndex(x => x.name == good.name);
                    game.ShopGoods.FindIndex(x => x.name == good.name);
                    if (playerGoodQTY <= 0)//玩家該貨物為零不能賣
                        continue;
                    if (!ignoreSalesRules && containGoodIndex >= 0)//沒有辦公室且重複的商品不能賣
                    {
                        //Console.WriteLine($"***沒有辦公室且重複的商品***");
                        continue; 
                    }
                    int buildingDiscount = 0;
                    //小市場作用時，只要有賣商品進商店，就可多得一元。可以和大市場一起作用（一起作用時多得三元）。
                    if (Func.CheckBuildingWithWorker(player1, typeof(Smallmarket)))
                        buildingDiscount += 1;
                    //大市場作用時，只要有賣商品進商店，就可多得二元。可以和小市場一起作用（一起作用時多得三元）
                    if (Func.CheckBuildingWithWorker(player1, typeof(Largemarket)))
                        buildingDiscount += 2;
                    //商人本身將商品賣給商店可多得1元（特權），但商人本身不做買賣時無法獲得這1元。
                    if (player1.Role == "Trader ")
                        buildingDiscount += 1;
                    int goodPrice = 0;
                    //玉米值0元，染料值1元，蔗糖值2元，煙草值3元，咖啡值4元
                    switch (good.name)
                    {
                        case "Indigo":
                            goodPrice = 1; break;
                        case "Sugar":
                            goodPrice = 2; break;
                        case "Tobacco":
                            goodPrice = 3; break;
                        case "Coffee":
                            goodPrice = 4; break;
                    }
                    int totalPrice = buildingDiscount + goodPrice;
                    int getMoneyFromBank = game.bank.minusMoney(totalPrice);
                    if (totalPrice == 0)//商品總賣價是零元就不賣
                        continue;
                    //SalesGoods
                    player1.Money += getMoneyFromBank;
                    player1.Goods.Find(x => x.name == good.name).qty -= 1;
                    game.ShopGoods.Add(good);
                    Console.Write($"\t\t{player1.Name} Sales {good.name} to shop, get {getMoneyFromBank} dollar from bank, ");
                    game.ShowShopGoods();
                    break;
                }
            }
            game.checkShopList();
        }
    }
    internal class Captain : Role
    {
        public Captain() { Name = "Captain"; Money = 0; }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");

        }
    }
    internal class Prospector1 : Role //礦工1
    {
        public Prospector1() { Name = "Prospecto1"; Money = 0; }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            player.GetMoneyFromBank(1);
        }
    }
    internal class Prospector2 : Role //礦工1
    {
        public Prospector2() { Name = "Prospecto2"; Money = 0; }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            player.GetMoneyFromBank(1);
        }
    }





}

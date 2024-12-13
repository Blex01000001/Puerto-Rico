using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Puerto_Rico
{
    public class Game
    {
        public int PlayerNum;
        //public List<Player> players = new List<Player>();//玩家人數List
        public List<RoleAbstract> availableRoles;//角色List
        public List<RoleAbstract> selectedRoles = new List<RoleAbstract>();//角色List
        public List<Building> availableFarms = new List<Building>();
        public List<Building> HideFarms = new List<Building>();
        public List<Building> TrushFarms = new List<Building>();
        public List<Building> quarryFields = new List<Building>();
        public List<Building> availableBuildings = new List<Building>();
        public List<Goods> Goods = new List<Goods>();
        public List<Goods> ShopGoods = new List<Goods>();//商店四格商品的空間
        public List<Ship> ShipList = new List<Ship>();

        public Bank bank = new Bank();

        public int playerNum;
        public bool EndGame = false;
        public int Round = 0;
        public int MoneyBank;
        public int Score;

        public Game(int PlayerNum)
        {
            //Func.TransToBuildingType();//建立字典
            this.playerNum = PlayerNum;
            bank.moneySetUp();
            CreatePlayers(PlayerNum);
            CreateRoles(PlayerNum);
            SetWorker(PlayerNum);
            SetShip(PlayerNum);
            //SetGoods();
            CreateField();//建立所有農田物件
            CreateBuildings();//建立所有廠房、建築物物件
            GameSetUp(PlayerNum);

           

            //Console.WriteLine($"players.Count: {players.Count}");
            while (!EndGame)
            {
                Console.WriteLine($"==========ROUND {Round + 1}==========");

                availableRoles = availableRoles.OrderBy(x => Func.RndNum()).ToList();
                foreach (Player player in Player.list)
                {
                    Console.WriteLine($"{player.Name} select {availableRoles[0].Name}");
                    player.Role = availableRoles[0].Name;
                    if (availableRoles[0].Money > 0)//玩家所選的角色牌上如果有錢就加到玩家裡
                    {
                        player.Money += availableRoles[0].Money;
                        Console.WriteLine($"\t{player.Name} get {availableRoles[0].Money} money from Role, {player.Name} Sum Money: {player.Money}, Bank: {bank.Money}");
                        availableRoles[0].Money = 0;//角色牌所累積的錢歸零
                    }
                    availableRoles[0].Action(player, this);
                    selectedRoles.Add(availableRoles[0]);
                    availableRoles.Remove(availableRoles[0]);
                }

                Console.WriteLine($"==========ROUND {Round + 1} END==========");

                foreach (RoleAbstract roles in availableRoles)//沒有被選到的角色的錢+1
                {
                    Console.WriteLine($"remaining roles: {roles.Name} Money +1");
                    roles.Money += bank.minusMoney(1);
                }

                availableRoles.AddRange(selectedRoles);//將被選過的角色加回去availableRoles
                selectedRoles.RemoveAll(x => true);

                ShowAvailableRolesStatus();
                ShowAvailableFarms();
                //ShowHideFarms();
                ShowBankStatus();
                ShowShopGoods();
                ShowPlayerStatus();

                Player.NextGovernor();//換下一個人當總督
                Player.ClearPlayerRoles();//清空每個人所選的角色
                Console.WriteLine("\n");
                Round++;
            }
            //SetScore(PlayerNum);
        }
        private void GameSetUp(int playerNum)
        {
            //遊戲一開始每個人分得N-1元貨幣，N為遊戲人數。這些錢就放在各自島嶼板上的空位讓大家看到
            foreach (Player player in Player.list)
            {
                player.Money += bank.minusMoney(playerNum - 1);
            }
            //根據參加人數不同，每個人得到的第一個農田方塊不同：
            //3個人遊玩：第1、2家為染料田，第3家為玉米田。
            //4個人遊玩：第1、2家為染料田，第3、4家為玉米田。
            //5個人遊玩：第1、2、3家為染料田，第4、5家為玉米田。
            Func.shift(HideFarms.Find(x => x.GetType() == typeof(IndigoFarm)), Player.list[0].FarmList, HideFarms);
            Func.shift(HideFarms.Find(x => x.GetType() == typeof(IndigoFarm)), Player.list[1].FarmList, HideFarms);
            switch (playerNum)
            {
                case 3:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(CornFarm)), Player.list[2].FarmList, HideFarms);
                    break;
                case 4:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(CornFarm)), Player.list[2].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(CornFarm)), Player.list[3].FarmList, HideFarms);
                    break;
                case 5:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(IndigoFarm)), Player.list[2].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(CornFarm)), Player.list[3].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(CornFarm)), Player.list[4].FarmList, HideFarms);
                    break;
            }
            HideFarms = HideFarms.OrderBy(x => Func.RndNum()).ToList();
            availableFarms = HideFarms.Take(playerNum + 1).OrderBy(x => Func.RndNum()).ToList();
            foreach (var item in availableFarms)
            {
                HideFarms.Remove(item);
            }
        }
        private void CreateField()
        {
            for (int i = 0; i < 8; i++)
            {
                Building quarry = new QuarryFarm();
                quarryFields.Add(quarry);
            }
            for (int i = 0; i < 8; i++)
            {
                Building coffee = new CoffeeFarm();
                HideFarms.Add(coffee);
            }
            for (int i = 0; i < 9; i++)
            {
                Building tobacco = new TobaccoFarm();
                HideFarms.Add(tobacco);
            }
            for (int i = 0; i < 10; i++)
            {
                Building corn = new CornFarm();
                HideFarms.Add(corn);
            }
            for (int i = 0; i < 11; i++)
            {
                Building sugar = new SugarFarm();
                HideFarms.Add(sugar);
            }
            for (int i = 0; i < 12; i++)
            {
                Building indigo = new IndigoFarm();
                HideFarms.Add(indigo);
            }
        }
        private void CreateBuildings()
        {
            //生廠廠房
            for (int i = 0; i < 4; i++)
            {
                IndigoPlant indigoPlant_S = new IndigoPlant(0);
                availableBuildings.Add(indigoPlant_S);
            }
            for (int i = 0; i < 3; i++)
            {
                IndigoPlant indigoPlant_B = new IndigoPlant(1);
                availableBuildings.Add(indigoPlant_B);
            }
            for (int i = 0; i < 4; i++)
            {
                SugarMill sugarMill_S = new SugarMill(0);
                availableBuildings.Add(sugarMill_S);
            }
            for (int i = 0; i < 3; i++)
            {
                SugarMill sugarMill_B = new SugarMill(1);
                availableBuildings.Add(sugarMill_B);
            }
            for (int i = 0; i < 3; i++)
            {
                TobaccoStorage tobaccoStorage = new TobaccoStorage();
                availableBuildings.Add(tobaccoStorage);
            }
            for (int i = 0; i < 3; i++)
            {
                CoffeeRoaster coffeeRoaster = new CoffeeRoaster();
                availableBuildings.Add(coffeeRoaster);
            }
            //小型特殊功能建築
            for (int i = 0; i < 2; i++)
            {
                Smallmarket smallmarket = new Smallmarket(0);
                availableBuildings.Add(smallmarket);
            }
            for (int i = 0; i < 2; i++)
            {
                Largemarket largemarket = new Largemarket(0);
                availableBuildings.Add(largemarket);
            }
            for (int i = 0; i < 2; i++)
            {
                Hacienda hacienda = new Hacienda(0);
                availableBuildings.Add(hacienda);
            }
            for (int i = 0; i < 2; i++)
            {
                Constructionhut constructionhut = new Constructionhut(0);
                availableBuildings.Add(constructionhut);
            }
            for (int i = 0; i < 2; i++)
            {
                Smallwarehouse smallwarehouse = new Smallwarehouse(0);
                availableBuildings.Add(smallwarehouse);
            }
            for (int i = 0; i < 2; i++)
            {
                Largewarehouse largewarehouse = new Largewarehouse(0);
                availableBuildings.Add(largewarehouse);
            }
            for (int i = 0; i < 2; i++)
            {
                Hospice hospice = new Hospice(0);
                availableBuildings.Add(hospice);
            }
            for (int i = 0; i < 2; i++)
            {
                Office office = new Office(0);
                availableBuildings.Add(office);
            }
            for (int i = 0; i < 2; i++)
            {
                Factory factory = new Factory(0);
                availableBuildings.Add(factory);
            }
            for (int i = 0; i < 2; i++)
            {
                University university = new University(0);
                availableBuildings.Add(university);
            }
            for (int i = 0; i < 2; i++)
            {
                Harbor harbor = new Harbor(0);
                availableBuildings.Add(harbor);
            }
            for (int i = 0; i < 2; i++)
            {
                Wharf wharf = new Wharf(0);
                availableBuildings.Add(wharf);
            }
            //大型特殊功能建築
            Guildhall guildhall = new Guildhall(0);
            availableBuildings.Add(guildhall);
            Residence residence = new Residence(0);
            availableBuildings.Add(residence);
            Fortress fortress = new Fortress(0);
            availableBuildings.Add(fortress);
            Customshouse customshouse = new Customshouse(0);
            availableBuildings.Add(customshouse);
            Cityhall cityhall = new Cityhall(0);
            availableBuildings.Add(cityhall);
            //空的建築物，當作PASS
            PassBuilding passBuilding = new PassBuilding();
            availableBuildings.Add(passBuilding);

            availableBuildings = availableBuildings.OrderBy(x => Func.RndNum()).ToList();
        }
        private void SetGoods()
        {
            Goods.Add(new Corn(10));
            Goods.Add(new Sugar(11));
            Goods.Add(new Coffee(9));
            Goods.Add(new Tobacco(9));
            Goods.Add(new Indigo(11));
        }
        private void CreateRoles(int playerNum)
        {
            availableRoles = new List<RoleAbstract>();

            RoleAbstract Settler = new Settler();//開拓者
            availableRoles.Add(Settler);

            RoleAbstract Mayor = new Mayor();//市長
            availableRoles.Add(Mayor);

            RoleAbstract Builder = new Builder();//建築師
            availableRoles.Add(Builder);

            RoleAbstract Craftsman = new Craftsman();//工匠
            availableRoles.Add(Craftsman);

            RoleAbstract Trader = new Trader();//交易商
            availableRoles.Add(Trader);

            RoleAbstract Captain = new Captain();//船長
            availableRoles.Add(Captain);

            switch (playerNum)//探勘者
            {
                case 4:
                    RoleAbstract Prospector41 = new Prospector();
                    availableRoles.Add(Prospector41);
                    break;
                case 5:
                    RoleAbstract Prospector51 = new Prospector();
                    availableRoles.Add(Prospector51);
                    RoleAbstract Prospector52 = new Prospector();
                    availableRoles.Add(Prospector52);
                    break;
            }
        }
        private void CreatePlayers(int playerNum)
        {
            //Console.WriteLine("CreatePlayers");
            for (int i = 0; i < playerNum; i++)
            {
                Player player = new Player(Convert.ToChar(65 + i).ToString(), this);
            }
        }
        private void SetScore(int playerNum)
        {
            switch (playerNum)
            {
                case 3:
                    Score = 75;
                    break;
                case 4:
                    Score = 100;
                    break;
                case 5:
                    Score = 122;
                    break;
            }
        }
        private void SetWorker(int playerNum)
        {
            bank.WorkerShip = playerNum;
            //移民數量：55移民/3人  75移民/4人   95移民/5人
            switch (playerNum)
            {
                case 3:
                    bank.Worker = 55;
                    break;
                case 4:
                    bank.Worker = 75;
                    break;
                case 5:
                    bank.Worker = 95;
                    break;
            }
        }
        private void SetShip(int playerNum)
        {
            if(playerNum == 3)
            {
                ShipList.Add(new Ship(4));
                ShipList.Add(new Ship(5));
                ShipList.Add(new Ship(6));
            }else if(playerNum == 4)
            {
                ShipList.Add(new Ship(5));
                ShipList.Add(new Ship(6));
                ShipList.Add(new Ship(7));
            }
            else
            {
                ShipList.Add(new Ship(6));
                ShipList.Add(new Ship(7));
                ShipList.Add(new Ship(8));
            }
        }
        public void ShowAvailableRolesStatus()
        {
            Console.WriteLine("--------availableRoles status--------");
            Console.WriteLine($"Role\t\tMoney");
            foreach (RoleAbstract roles in availableRoles)
            {
                Console.WriteLine($"{roles.Name} \t  {roles.Money}");
            }
        }
        public void ShowAvailableFarms()
        {
            Console.WriteLine("--------availableFarm status--------");
            Console.WriteLine($"Farm\tWorks");
            foreach (Building farm in availableFarms)
            {
                Console.WriteLine($"{farm.Name}({farm.GetHashCode()}) \t{farm.worker}");
            }
        }
        public void ShowHideFarms()
        {
            Console.WriteLine("--------hideFarm status--------");
            Console.WriteLine($"Farm\tWorks");
            foreach (Building farm in HideFarms)
            {
                Console.WriteLine($"{farm.Name}({farm.GetHashCode()}) \t{farm.worker}");
            }
        }

        public void ShowBankStatus()
        {
            Console.WriteLine("--------Bank status--------");
            Console.WriteLine($"Item      \tQTY");

            Console.WriteLine($"WorkerShip\t{bank.WorkerShip}");
            Console.WriteLine($"Worker    \t{bank.Worker}");
            Console.WriteLine($"Money     \t{bank.Money}");
            Console.WriteLine($"Corn      \t{bank.GoodList[0].qty}");
            Console.WriteLine($"Sugar     \t{bank.GoodList[1].qty}");
            Console.WriteLine($"Coffee    \t{bank.GoodList[2].qty}");
            Console.WriteLine($"Tobacco   \t{bank.GoodList[3].qty}");
            Console.WriteLine($"Indigo    \t{bank.GoodList[4].qty}");
        }
        public void ShowShopGoods()
        {
            Console.Write($"ShopGoods:\t");
            foreach (Goods good in ShopGoods)
            {
                Console.Write($" {good.name}");
            }
            Console.Write($"\n");

        }

        public void ShowPlayerStatus()
        {
            Console.WriteLine("--------player status--------");
            Console.WriteLine($"Name\tScore\tMoney\tWorker\tCorn\tSugar\tCoffee\tTobacco\tIndigo\t");
            foreach (Player player in Player.list)
            {
                Console.Write($"{player.Name}    \t{player.Score}   \t{player.Money}   \t{player.Worker}    \t{player.Goods[0].qty}   \t{player.Goods[1].qty}    \t{player.Goods[2].qty}     \t{player.Goods[3].qty}      \t{player.Goods[4].qty}\n");
            }
            Console.Write("\n");
            Console.Write($"field:\n");
            foreach (Player player in Player.list)
            {
                Console.Write($"{player.Name} ");
                foreach (Building field in player.FarmList)
                {
                    Console.Write($"{field.Name}({field.GetHashCode()})({field.worker}/{field.MaxWorker})\t, ");
                }
                Console.Write($"\n");
            }

            Console.Write($"\nbuilding:\n");
            foreach (Player player in Player.list)
            {
                Console.Write($"{player.Name} ");
                foreach (Building building in player.BuildingList)
                {
                    Console.Write($"{building.Name} ({building.GetHashCode()}) ({building.worker}/{building.MaxWorker}), ");
                }
                Console.Write($"\n");
            }
        }

        public void ArrangeFarms()
        {
            for (int i = 0; i < availableFarms.Count; i++)
            {
                TrushFarms.Add(availableFarms[i]);
                TrushFarms.OrderBy(x => Func.RndNum());
            }
            availableFarms.Clear();

            while (HideFarms.Count < playerNum + 1)
            {
                if (TrushFarms.Count <= 0)
                    break;
                foreach (Building farm in TrushFarms)
                {
                    HideFarms.Add(farm);
                }
                TrushFarms.Clear();
            }

            if(HideFarms.Count >= playerNum + 1)
            {
                availableFarms = HideFarms.Take(playerNum + 1).ToList();
            }
            else
            {
                availableFarms = HideFarms.Take(HideFarms.Count).ToList();
            }


            for (int i = 0; i < availableFarms.Count; i++)
            {
                HideFarms.Remove(availableFarms[i]);
            }


        }
        public void RndAvailableBuildings()
        {
            availableBuildings = availableBuildings.OrderBy(x => Func.RndNum()).ToList();
        }
        public void checkShopList()
        {
            if(ShopGoods.Count >= 4)
                ShopGoods.Clear();
        }
    }
}

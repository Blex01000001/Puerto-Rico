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
    internal class Game
    {
        public int PlayerNum;
        //public List<Player> players = new List<Player>();//玩家人數List
        public List<Role> availableRoles;//角色List
        public List<Role> selectedRoles = new List<Role>();//角色List
        public List<Building> availableFarms = new List<Building>();
        public List<Building> HideFarms = new List<Building>();
        public List<Building> TrushFarms = new List<Building>();
        public List<Building> quarryFields = new List<Building>();
        public List<Building> availableBuildings = new List<Building>();

        public Bank bank = new Bank();


        public int playerNum;
        public bool EndGame = false;
        public int Round = 0;
        public Goods goods;
        public int MoneyBank;
        public int Score;

        public Game(int PlayerNum)
        {
            this.playerNum = PlayerNum;
            bank.moneySetUp();
            CreatePlayers(PlayerNum);
            CreateRoles(PlayerNum);
            SetWorker(PlayerNum);
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
                        Console.WriteLine($"{player.Name} get {availableRoles[0].Money} money from Role, {player.Name} Sum Money: {player.Money}, Bank: {bank.Money}");
                        availableRoles[0].Money = 0;//角色牌所累積的錢歸零
                    }
                    availableRoles[0].action(player, this);
                    selectedRoles.Add(availableRoles[0]);
                    availableRoles.Remove(availableRoles[0]);
                }

                Console.WriteLine($"==========ROUND {Round + 1} END==========");

                foreach (Role roles in availableRoles)//沒有被選到的角色的錢+1
                {
                    Console.WriteLine($"remaining roles: {roles.Name} Money +1");
                    roles.Money += bank.minusMoney(1);
                }

                availableRoles.AddRange(selectedRoles);//將被選過的角色加回去availableRoles
                selectedRoles.RemoveAll(x => true);

                showAvailableRolesStatus();
                showAvailableFarms();
                //showHideFarms();
                showBankStatus();
                showPlayerStatus();

                Player.nextGovernor();//換下一個人當總督
                Player.clearPlayerRoles();//清空每個人所選的角色
                Console.WriteLine("\n");
                Round++;
            }
            //SetScore(PlayerNum);
            //SetWorker(PlayerNum);
            //SetMoney(PlayerNum);
            //SetGoods();
            //SetQuarryfield();
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
            Func.shift(HideFarms.Find(x => x.GetType() == typeof(Indigo)), Player.list[0].FarmList, HideFarms);
            Func.shift(HideFarms.Find(x => x.GetType() == typeof(Indigo)), Player.list[1].FarmList, HideFarms);
            switch (playerNum)
            {
                case 3:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), Player.list[2].FarmList, HideFarms);
                    break;
                case 4:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), Player.list[2].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), Player.list[3].FarmList, HideFarms);
                    break;
                case 5:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Indigo)), Player.list[2].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), Player.list[3].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), Player.list[4].FarmList, HideFarms);
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
                Building quarry = new Quarry();
                quarryFields.Add(quarry);
            }
            for (int i = 0; i < 8; i++)
            {
                Building coffee = new Coffee();
                HideFarms.Add(coffee);
            }
            for (int i = 0; i < 9; i++)
            {
                Building tobacco = new Tobacco();
                HideFarms.Add(tobacco);
            }
            for (int i = 0; i < 10; i++)
            {
                Building corn = new Corn();
                HideFarms.Add(corn);
            }
            for (int i = 0; i < 11; i++)
            {
                Building sugar = new Sugar();
                HideFarms.Add(sugar);
            }
            for (int i = 0; i < 12; i++)
            {
                Building indigo = new Indigo();
                HideFarms.Add(indigo);
            }
        }
        private void CreateBuildings()
        {
            //工廠
            for (int i = 0; i < 3; i++)
            {
                IndigoPlant_B indigoPlant_B = new IndigoPlant_B();
                availableBuildings.Add(indigoPlant_B);
            }
            for (int i = 0; i < 4; i++)
            {
                IndigoPlant_S indigoPlant_S = new IndigoPlant_S();
                availableBuildings.Add(indigoPlant_S);
            }
            for (int i = 0; i < 3; i++)
            {
                SugarMill_B sugarMill_B = new SugarMill_B();
                availableBuildings.Add(sugarMill_B);
            }
            for (int i = 0; i < 4; i++)
            {
                SugarMill_S sugarMill_S = new SugarMill_S();
                availableBuildings.Add(sugarMill_S);
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
            //建築物
            for (int i = 0; i < 3; i++)
            {
                Smallmarket smallmarket = new Smallmarket();
                availableBuildings.Add(smallmarket);
            }

            //空的建築物，當作PASS
            PassBuilding passBuilding = new PassBuilding();
            availableBuildings.Add(passBuilding);

            availableBuildings = availableBuildings.OrderBy(x => Func.RndNum()).ToList();
        }
        private void SetGoods()
        {
            goods = new Goods();
        }
        private void CreateRoles(int playerNum)
        {
            availableRoles = new List<Role>();

            Role Settler = new Settler();//開拓者
            availableRoles.Add(Settler);

            Role Mayor = new Mayor();//市長
            availableRoles.Add(Mayor);

            Role Builder = new Builder();//建築師
            availableRoles.Add(Builder);

            Role Craftsman = new Craftsman();//工匠
            availableRoles.Add(Craftsman);

            Role Trader = new Trader();//交易商
            availableRoles.Add(Trader);

            Role Captain = new Captain();//船長
            availableRoles.Add(Captain);

            switch (playerNum)//探勘者
            {
                case 4:
                    Role Prospector41 = new Prospector1();
                    availableRoles.Add(Prospector41);
                    break;
                case 5:
                    Role Prospector51 = new Prospector1();
                    availableRoles.Add(Prospector51);
                    Role Prospector52 = new Prospector2();
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
        public void showAvailableRolesStatus()
        {
            Console.WriteLine("--------availableRoles status--------");
            Console.WriteLine($"Role\t\tMoney");
            foreach (Role roles in availableRoles)
            {
                Console.WriteLine($"{roles.Name} \t  {roles.Money}");
            }
        }
        public void showAvailableFarms()
        {
            Console.WriteLine("--------availableFarm status--------");
            Console.WriteLine($"Farm\tWorks");
            foreach (Building farm in availableFarms)
            {
                Console.WriteLine($"{farm.Name}({farm.GetHashCode()}) \t{farm.worker}");
            }
        }
        public void showHideFarms()
        {
            Console.WriteLine("--------hideFarm status--------");
            Console.WriteLine($"Farm\tWorks");
            foreach (Building farm in HideFarms)
            {
                Console.WriteLine($"{farm.Name}({farm.GetHashCode()}) \t{farm.worker}");
            }
        }

        public void showBankStatus()
        {
            Console.WriteLine("--------Bank status--------");
            Console.WriteLine($"Item      \tQTY");

            Console.WriteLine($"WorkerShip\t{bank.WorkerShip}");
            Console.WriteLine($"Worker    \t{bank.Worker}");
            Console.WriteLine($"Money     \t{bank.Money}");
        }

        public void showPlayerStatus()
        {
            Console.WriteLine("--------player status--------");
            Console.WriteLine($"Name \tMoney \tWorker");
            foreach (Player player in Player.list)
            {
                Console.Write($"{player.Name}    \t  {player.Money} \t  {player.Worker}\n");
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
    }
}

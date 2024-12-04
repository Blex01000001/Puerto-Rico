using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Game
    {
        public int PlayerNum;
        public List<Player> players = new List<Player>();//玩家人數List
        public List<Role> availableRoles;//角色List
        public List<Role> selectedRoles = new List<Role>();//角色List
        public List<Farm> availableFarms = new List<Farm>();
        public List<Farm> HideFarms = new List<Farm>();
        public List<Farm> TrushFarms = new List<Farm>();
        public List<Farm> quarryFields = new List<Farm>();
        
        public Bank bank = new Bank();

        public int playerNum;


        public Goods goods;
        public int MoneyBank;
        public int Score;

        public Game(int PlayerNum)
        {
            this.playerNum = PlayerNum;
            CreatePlayers(PlayerNum);
            CreateRoles(PlayerNum);
            SetWorker(PlayerNum);
            SetUp(PlayerNum);

            //Console.WriteLine($"players.Count: {players.Count}");

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"==========ROUND {i+1}==========");

                availableRoles = availableRoles.OrderBy(x => Func.RndNum()).ToList();
                foreach (Player player in players)
                {
                    Console.WriteLine($"Player {player.Name} select {availableRoles[0].Name}");
                    player.Money += availableRoles[0].Money;//玩家所選的角色牌上如果有錢就加到玩家裡
                    availableRoles[0].Money = 0;//角色牌所累積的錢歸零
                    //showAvailableFarms();
                    //showPlayerStatus();
                    availableRoles[0].action(player, this);
                    //showAvailableFarms();
                    //showPlayerStatus();
                    selectedRoles.Add(availableRoles[0]);
                    availableRoles.Remove(availableRoles[0]);
                }

                Console.WriteLine($"==========ROUND {i+1} END==========");

                foreach (Role roles in availableRoles)//沒有被選到的角色的錢+1
                {
                    Console.WriteLine($"remaining roles: {roles.Name} Money +1");
                    roles.Money += 1;
                }

                availableRoles.AddRange(selectedRoles);//將被選過的角色加回去availableRoles
                selectedRoles.RemoveAll(x => true);

                showAvailableRolesStatus();
                showAvailableFarms();
                //showHideFarms();
                showBankStatus();
                showPlayerStatus();

                players.Add(players[0]);//將第一人移至最後
                players.RemoveAt(0);//刪除第一人

                Console.WriteLine("");
                Console.WriteLine("");

            }
            //SetScore(PlayerNum);
            //SetWorker(PlayerNum);
            //SetMoney(PlayerNum);
            //SetMoney(PlayerNum);
            //SetGoods();
            //
            //SetQuarryfield();
        }
        private void SetUp(int playerNum)
        {
            //遊戲一開始每個人分得N-1元貨幣，N為遊戲人數。這些錢就放在各自島嶼板上的空位讓大家看到
            foreach (Player player in players)
            {
                player.Money += playerNum - 1;
            }

            SetFieldNum();//建立所有農田
            //根據參加人數不同，每個人得到的第一個農田方塊不同：
            //3個人遊玩：第1、2家為染料田，第3家為玉米田。
            //4個人遊玩：第1、2家為染料田，第3、4家為玉米田。
            //5個人遊玩：第1、2、3家為染料田，第4、5家為玉米田。
            Func.shift(HideFarms.Find(x => x.GetType() == typeof(Indigo)), players[0].FarmList, HideFarms);
            Func.shift(HideFarms.Find(x => x.GetType() == typeof(Indigo)), players[1].FarmList, HideFarms);
            switch (playerNum)
            {
                case 3:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), players[2].FarmList, HideFarms);
                    break;
                case 4:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), players[2].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), players[3].FarmList, HideFarms);
                    break;
                case 5:
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Indigo)), players[2].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), players[3].FarmList, HideFarms);
                    Func.shift(HideFarms.Find(x => x.GetType() == typeof(Corn)), players[4].FarmList, HideFarms);
                    break;
            }
            HideFarms = HideFarms.OrderBy(x => Func.RndNum()).ToList();
            availableFarms = HideFarms.Take(playerNum + 1).OrderBy(x => Func.RndNum()).ToList();
            foreach (var item in availableFarms)
            {
                HideFarms.Remove(item);
            }
        }
        private void SetFieldNum()
        {

            for (int i = 0; i < 8; i++)
            {
                Farm quarry = new Quarry();
                quarryFields.Add(quarry);
            }
            for (int i = 0; i < 8; i++)
            {
                Farm coffee = new Coffee();
                HideFarms.Add(coffee);
            }
            for (int i = 0; i < 9; i++)
            {
                Farm tobacco = new Tobacco();
                HideFarms.Add(tobacco);
            }
            for (int i = 0; i < 10; i++)
            {
                Farm corn = new Corn();
                HideFarms.Add(corn);
            }
            for (int i = 0; i < 11; i++)
            {
                Farm sugar = new Sugar();
                HideFarms.Add(sugar);
            }
            for (int i = 0; i < 12; i++)
            {
                Farm indigo = new Indigo();
                HideFarms.Add(indigo);
            }
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
            Console.WriteLine("CreatePlayers");
            for (int i = 0; i < playerNum; i++)
            {
                Player player = new Player(Convert.ToChar(65 + i).ToString());
                players.Add(player);
            }
        }
        private void SetMoney(int playerNum)
        {
            MoneyBank = 40 + 46;
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
            foreach (Farm farm in availableFarms)
            {
                Console.WriteLine($"{farm.Name}({farm.GetHashCode()}) \t{farm.worker}");
            }
        }
        public void showHideFarms()
        {
            Console.WriteLine("--------hideFarm status--------");
            Console.WriteLine($"Farm\tWorks");
            foreach (Farm farm in HideFarms)
            {
                Console.WriteLine($"{farm.Name}({farm.GetHashCode()}) \t{farm.worker}");
            }
        }

        public void showBankStatus()
        {
            Console.WriteLine("--------Bank status--------");
            Console.WriteLine($"Item\t\t\tQTY");

            Console.WriteLine($"WorkerShip\t\t{bank.WorkerShip}");
            Console.WriteLine($"Worker\t\t\t{bank.Worker}");
        }

        public void showPlayerStatus()
        {
            Console.WriteLine("--------player status--------");
            Console.WriteLine($"Name \tMoney \tWorker");
            foreach (Player player in players)
            {
                Console.Write($"{player.Name}    \t  {player.Money} \t  {player.Worker}");
                foreach (Farm field in player.FarmList)
                {
                    Console.Write($"\t\t{field.Name}({field.GetHashCode()}) ({field.worker}/{field.MaxWorker})");
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
                foreach (Farm farm in TrushFarms)
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

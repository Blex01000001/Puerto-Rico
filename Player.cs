using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Puerto_Rico
{
    public class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Worker { get; set; }
        public int Score { get; set; }
        public List<Goods> Goods = new List<Goods>();
        public string Role;
        public List<Building> FarmList = new List<Building>();
        public List<Building> BuildingList = new List<Building>();
        public Game game;
        static public List<Player> list = new List<Player>();
        //static public List<Player> PlayerListFromRole = new List<Player>();

        public Player(string name, Game game)
        {
            Goods.Add(new Corn(0));
            Goods.Add(new Sugar(0));
            Goods.Add(new Coffee(0));
            Goods.Add(new Tobacco(0));
            Goods.Add(new Indigo(0));
            Player.list.Add(this);
            this.Name = name;
            this.game = game;
            this.Money = 0;
            this.Worker = 0;
        }
        public List<Building> GetAllBuildings()
        {
            List<Building> allBuildings = new List<Building>();
            allBuildings.AddRange(BuildingList);
            allBuildings.AddRange(FarmList);
            return allBuildings;
        }
        static public List<Player> GetPlayerListFromRole(Player player)
        {
            List<Player> templist = new List<Player>();
            int playerIndex = list.FindIndex(x => x == player);
            for (int i = 0; i < list.Count; i++)
            {
                int ii = (i % list.Count) + playerIndex > (list.Count - 1) ? (i % list.Count) + playerIndex - list.Count : (i % list.Count) + playerIndex;
                templist.Add(list[ii]);
            }
            return templist;
        }
        static public void ClearPlayerRoles()
        {
            foreach (Player player in list)
            {
                player.Role = "";
            }
        }
        static public void NextGovernor()
        {
            Player.list.Add(Player.list[0]);//將第一人移至最後
            Player.list.RemoveAt(0);//刪除第一人

        }
        public List<Building> GetEmptyCircleList()
        {
            List<Building> PlayerBuildings = new List<Building>();
            //Console.WriteLine($"FarmList.count: {FarmList.Count}");
            foreach (Building farm in FarmList)
            {
                for (int i = 0; i < farm.MaxWorker; i++)
                {
                    PlayerBuildings.Add(farm);
                }
            }

            foreach (Building Building in BuildingList)
            {
                for (int i = 0; i < Building.MaxWorker; i++)
                {
                    PlayerBuildings.Add(Building);
                }
            }
            //Console.WriteLine($"PlayerBuildings.count: {PlayerBuildings.Count}");

            PlayerBuildings = PlayerBuildings.OrderBy(x => Func.RndNum()).ToList();
            return PlayerBuildings;
        }

        public void ClearFarmWorker()
        {
            foreach (Building farm in FarmList)
            {
                farm.worker = 0;
            }
        }
        public void ClearFactoryWorker()
        {
            foreach (Building Building in BuildingList)
            {
                Building.worker = 0;
            }
        }
        public void GetMoneyFromBank(int money)
        {
            int getMoney = game.bank.minusMoney(money);
            this.Money += getMoney;
            Console.WriteLine($" {Name} get {getMoney} Money, {Name} Sum Money: {Money}, Bank: {game.bank.Money}");

        }
        public int GetFarmWorker(string IndustryType)
        {
            return FarmList.Where(x => x.Industry == IndustryType).Where(x => x.worker > 0).Sum(x => x.worker);
        }
        public int GetBuildingWorker(string IndustryType)
        {
            return BuildingList.Where(x => x.Industry == IndustryType).Where(x => x.worker > 0).Sum(x => x.worker);
        }

    }
}

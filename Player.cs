using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Puerto_Rico
{
    internal class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Worker { get; set; }
        public string Role;
        public List<Building> FarmList = new List<Building>();
        //public List<Building> FactoryList = new List<Building>();
        public List<Building> BuildingList = new List<Building>();
        public Game game;
        static public List<Player> list = new List<Player>();

        public Player(string name, Game game)
        {
            Player.list.Add(this);
            this.Name = name;
            this.game = game;
            this.Money = 0;
            this.Worker = 0;
        }
        static public void clearPlayerRoles()
        {
            foreach (Player player in list)
            {
                player.Role = "";
            }
        }
        static public void nextGovernor()
        {
            Player.list.Add(Player.list[0]);//將第一人移至最後
            Player.list.RemoveAt(0);//刪除第一人

        }
        public List<Building> getEmptyCircleList()
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

        public void clearFarmWorker()
        {
            foreach (Building farm in FarmList)
            {
                farm.worker = 0;
            }
        }
        public void clearFactoryWorker()
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
    }
}

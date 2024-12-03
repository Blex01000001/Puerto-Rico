using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Puerto_Rico
{
    internal class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Worker { get; set; }
        public List<Farm> FarmList = new List<Farm>();
        public List<Factory> FactoryList = new List<Factory>();
        Random rnd = new Random(Guid.NewGuid().GetHashCode());


        public Player(string name)
        {
            this.Name = name;
            this.Money = 0;
        }

        public List<Building> getBuildingList()
        {
            List<Building> PlayerBuildings = new List<Building>();
            //Console.WriteLine($"FarmList.count: {FarmList.Count}");
            foreach (Farm farm in FarmList)
            {
                for (int i = 0; i < farm.MaxWorker; i++)
                {
                    PlayerBuildings.Add(farm);
                }
            }

            foreach (Factory factory in FactoryList)
            {
                for (int i = 0; i < factory.MaxWorker; i++)
                {
                    PlayerBuildings.Add(factory);
                }
            }
            //Console.WriteLine($"PlayerBuildings.count: {PlayerBuildings.Count}");

            PlayerBuildings = PlayerBuildings.OrderBy(x => rnd.Next()).ToList();
            return PlayerBuildings;
        }

        public void clearFarmWorker()
        {
            foreach (Farm farm in FarmList)
            {
                farm.worker = 0;
            }
        }
        public void clearFactoryWorker()
        {
            foreach (Factory factory in FactoryList)
            {
                factory.worker = 0;
            }
        }

    }
}

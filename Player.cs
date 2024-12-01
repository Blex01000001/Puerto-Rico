using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int Worker { get; set; }
        public List<Farm> FarmList = new List<Farm>();
        public List<Factory> FactoryList = new List<Factory>();

        public Player(string name)
        {
            this.Name = name;
            this.Money = 0;
        }
    }
}

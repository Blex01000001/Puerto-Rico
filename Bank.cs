using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Bank
    {
        public int Worker { get; set; }
        public int WorkerShip;

        public void getWorkerFromBank(Player player)
        {
            player.Worker += 1;
            Worker -= 1;
        }
        //public void getWorkerWorkerShip(Player player)
        //{
        //    player.Worker += 1;
        //    Worker -= 1;
        //}
    }
}

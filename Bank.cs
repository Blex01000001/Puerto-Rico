using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Puerto_Rico
{
    internal class Bank
    {
        public int Worker { get; set; }
        public int WorkerShip;
        public int Money { get { return _money; }}


        private int _money;

        public Bank()
        {
            Worker = 0;
        }


        public void getWorkerFromBank(Player player)
        {
            player.Worker += 1;
            Worker -= 1;
        }

        public void moneySetUp()
        {
            _money = 8 * 5 + 46 * 1;//86元
        }
        public int minusMoney(int money)
        {
            if (_money <= 0)
            {
                Console.WriteLine($"NO MONEY, Bank: {_money}");
                return 0;
            }
            if (_money < money)
            {
                _money = 0;
                Console.WriteLine($"NO MONEY, Bank: {_money}");
                return _money;
            }
            _money -= money;
            return money;
        }
        public void addMoney(int money)
        {
            _money += money;
        }

        //public void getWorkerWorkerShip(Player player)
        //{
        //    player.Worker += 1;
        //    Worker -= 1;
        //}
    }
}

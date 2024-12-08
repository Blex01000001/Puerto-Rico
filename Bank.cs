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

        public List<Goods> GoodList { get; }
        private int _money;

        public Bank()
        {
            GoodList = new List<Goods>();
            Corn corn = new Corn(10);
            Sugar sugar = new Sugar(11);
            Coffee coffee = new Coffee(9);
            Tobacco tobacco = new Tobacco(9);
            Indigo indigo = new Indigo(11);
            GoodList.Add(corn);
            GoodList.Add(sugar);
            GoodList.Add(coffee);
            GoodList.Add(tobacco);
            GoodList.Add(indigo);

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
                Console.WriteLine($"Bank NO MONEY, Bank: {_money}");
                return 0;
            }
            else if (_money < money)
            {
                int temp = _money;
                _money = 0;
                Console.WriteLine($"Bank MONEY not enough, Bank: {_money}");
                return temp;
            }
            _money -= money;
            return money;
        }
        public void addMoney(int money)
        {
            _money += money;
        }
        public int getGoods(Goods good, int qty)
        {
            return Goods.getGoods(good, qty);
        }

        //public void getWorkerWorkerShip(Player player)
        //{
        //    player.Worker += 1;
        //    Worker -= 1;
        //}
    }
}

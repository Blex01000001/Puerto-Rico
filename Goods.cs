using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Goods
    {
        public int qty;
        public string name;
        public Goods()
        {
        }
        static public int getGoods(Goods good, int qty)
        {
            //Goods goods = list.Find(x => x.GetType() = goodType);
            if (good.qty <= 0)
            {
                Console.WriteLine($" there are no {good.GetType().Name} in the bank, Bank {good.GetType().Name}: {good.qty}");
                return 0;
            }
            else if (good.qty < qty)
            {
                int temp = good.qty;
                good.qty = 0;
                Console.WriteLine($" only {temp} {good.GetType().Name} in the bank, Bank {good.GetType().Name}: {good.qty}");
                return temp;
            }
            good.qty -= qty;
            return qty;

        }
    }
    internal class Coffee : Goods
    {
        public Coffee(int qty)
        {
            name = "Coffee";
            this.qty = qty;
        }
    }
    internal class Tobacco : Goods
    {
        public Tobacco(int qty)
        {
            name = "Tobacco";
            this.qty = qty;
        }
    }
    internal class Corn : Goods
    {
        public Corn(int qty)
        {
            name = "Corn";
            this.qty = qty;
        }
    }
    internal class Sugar : Goods
    {
        public Sugar(int qty)
        {
            name = "Sugar";
            this.qty = qty;
        }
    }
    internal class Indigo : Goods
    {
        public Indigo(int qty)
        {
            name = "Indigo";
            this.qty = qty;
        }
    }


}

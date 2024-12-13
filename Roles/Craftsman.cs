using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    //工匠特權所獲得的那一個資源是否要等全部人都收成完才能拿? 還是在工匠收成時就可以直接拿?
    internal class Craftsman : RoleAbstract//工匠
    {
        public override string Name
        {
            get { return "Craftsman"; }
        }
        public override int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private int _money;
        public Craftsman()
        {
            _money = 0;
        }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            foreach (Player player1 in Player.GetPlayerListFromRole(player))
            {
                foreach (Goods good in game.bank.GoodList)
                {
                    int checkedHarvest = Func.CheckedHarvest(player1, good);
                    if (checkedHarvest <= 0)//小於0代表無法收成
                        continue;
                    int getGoodQTY = game.bank.getGoods(good, checkedHarvest);
                    player1.Goods.Find(x => x.name == good.name).qty += getGoodQTY;
                    Console.WriteLine($"\t\t{player1.Name} get {getGoodQTY} {good.name} from bank");
                }
            }
        }
    }
}

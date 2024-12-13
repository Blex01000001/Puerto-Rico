using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Prospector : RoleAbstract //礦工
    {
        public override string Name
        {
            get { return "Prospecto1"; }
        }
        public override int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private int _money;
        public Prospector()
        {
            _money = 0;
        }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            player.GetMoneyFromBank(1);
        }
    }
}

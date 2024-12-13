using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Captain : RoleAbstract //船長
    {
        public override string Name
        {
            get { return "Captain"; }
        }
        public override int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private int _money;
        public Captain()
        {
            _money = 0;
        }
        public override void Action(Player player, Game game)
        {
            //從船長開始，依照上下家次序，將各項物資運上貨船，以運回舊大陸。任何人依據規則，只要有物資可以運上船，就必須要將物資運上船。物資運輸的輪替會一直重複，直到所有人都無法將物資運上船為止。
            Console.WriteLine($"\t{Name} Action");
            foreach (Player p1 in Player.GetPlayerListFromRole(player))
            {
                List<Goods> playerRndGoods = p1.Goods.OrderBy(x => Func.RndNum()).ToList();
                foreach (Goods good in playerRndGoods)
                {
                    if (good.qty <= 0)
                        continue;
                    CheckCargoScore(p1, good, game);
                }
            }

        }
        private void CheckCargoScore(Player player, Goods good, Game game)
        {
            List<TransportStrategy> Strategies = new List<TransportStrategy>();
            foreach (Ship ship in game.ShipList)
            {
                Strategies.Add(new TransportStrategy(ship));
            }
            int topScore = Strategies.Max(y => y.Score);//找出分數最高的策略
            Strategies = Strategies.OrderBy(x => Func.RndNum()).ToList();//打亂策略順序
            Strategies.First(x => x.Score == topScore).Transport();//執行分數最高的策略
        }


    }

    internal class TransportStrategy
    {
        public int Score { get { return _score; } }
        private int _score;
        public TransportStrategy(Ship ship)
        {
            if (ship.Cargo == null)
            {

            }
        }
        public void Transport()
        {

        }

    }
}

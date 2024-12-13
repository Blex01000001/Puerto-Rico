using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    //移民不夠補充移民船時，是此次腳色輪轉後結束還是下一回合才結束?
    internal class Mayor : RoleAbstract //市長
    {
        public override string Name
        {
            get { return "Mayor  "; }
        }
        public override int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private int _money;
        public Mayor()
        {
            _money = 0;
        }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            Console.WriteLine($"\t{player.Name} get 1 worker(Mayor)");
            game.bank.getWorkerFromBank(player);//市長特權
            int playerIndex = Player.list.FindIndex(x => x == player);
            foreach (Player p1 in Player.GetPlayerListFromRole(player))//從市長開始每個人輪流拿工人，拿到移民船上沒有工人
            {
                p1.Worker += 1;
                Console.WriteLine($"\t{p1.Name} get 1 worker");
            }
            game.bank.WorkerShip = 0;
            int totalPlayerEmptyCircle = 0;
            foreach (Player p2 in Player.GetPlayerListFromRole(player))//所有人必須將得到的移民放在地圖上任何有圈圈的地方（包括農田方塊或者建築物上），而之前部署的任何移民，可以在此時重新部署（但仍然只要有空圈圈且有空間的移民就要部署）
            {
                List<Building> EmptyCircleList = p2.GetEmptyCircleList();
                p2.ClearFarmWorker();
                p2.ClearFactoryWorker();
                for (int i = 0; i < p2.Worker; i++)
                {
                    if (EmptyCircleList.Count <= 0)
                        break;
                    EmptyCircleList[0].worker += 1;
                    Console.WriteLine($"\t\t{p2.Name} put 1 worker on {EmptyCircleList[0].Name}({EmptyCircleList[0].GetHashCode()})");
                    EmptyCircleList.RemoveAt(0);
                }
                totalPlayerEmptyCircle += EmptyCircleList.Count;
            }
            game.bank.WorkerShip = totalPlayerEmptyCircle > game.playerNum ? totalPlayerEmptyCircle : game.playerNum;
            game.bank.Worker -= game.bank.WorkerShip;
            if (game.bank.Worker < 0)//移民不夠補充移民船時，則遊戲結束事件發生
            {
                Console.WriteLine("\n>>>>移民不夠補充移民船，遊戲將在角色輪轉後結束<<<<\n");
                game.EndGame = true;
            }
        }
    }
}

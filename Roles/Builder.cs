using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{    
    //建築師可以不建嗎?
    //其他玩家可以不建嗎?
    internal class Builder : RoleAbstract //建築師
    {
        public override string Name
        {
            get { return "Builder"; }
        }
        public override int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private int _money;
        public Builder()
        {
            _money = 0;
        }
        public override void Action(Player player, Game game)
        {
            Console.WriteLine($"\t{Name} Action");
            int playerIndex = Player.list.FindIndex(x => x == player);
            foreach (Player player1 in Player.GetPlayerListFromRole(player))//由建築師本身開始，可以依照上下家順序興建一座建築物。建築師可以在興建任何建築物的時候少花一元（最少可以免費；特權）。
            {
                for (int j = 0; j < game.availableBuildings.Count; j++)//逐一檢查每個建築
                {
                    if (game.availableBuildings[j].Name == "PassBuilding")//選到PassBuilding代表該玩家pass掉買建築物
                    {

                        Console.WriteLine($"\t\t{player1.Name} PASS buy the building");
                        break;
                    }
                    int containBuildingIndex = player1.GetAllBuildings().FindIndex(x => x.Name == game.availableBuildings[j].Name);
                    if (containBuildingIndex >= 0)//檢查玩家有沒有重複的建築
                        continue;
                    int buildingCost = Func.checkDis(player1, game.availableBuildings[j]);
                    if (buildingCost < 0) //買不起就換下一個建築
                        continue;
                    //買得起就直接買
                    Console.WriteLine($"\t\t{player1.Name} cost {buildingCost} buy the {game.availableBuildings[j].Name}");
                    player1.BuildingList.Add(game.availableBuildings[j]);
                    game.availableBuildings.Remove(game.availableBuildings[j]);
                    //扣錢還給銀行
                    player1.Money -= buildingCost;
                    game.bank.addMoney(buildingCost);
                    break;
                }
                game.RndAvailableBuildings();
            }
        }
    }
}

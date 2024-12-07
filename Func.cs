using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Func
    {
        static public void shift<T>(T item, List<T> AddToList, List<T> RemoveFromList)
        {
            AddToList.Add(item);
            RemoveFromList.Remove(item);
        }

        static public int RndNum()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            return rnd.Next();
        }
        static public int checkDis(Player player, Building building)
        {
            //判斷玩家是否可以買這個建築物(包含打折)，買得起就返回打折後的價格，買不起就返回-1
            int playerOreWithWorker = player.FarmList.Where(x => x.Name == "Ore").Where(x => x.worker > 0).ToList().Count;
            int playerOreDiscount = 0;
            int playerIsBuilderDiscount = 0;
            for (int i = 1; i < building.Level; i++)//檢查玩家的礦廠可以折幾元
                {
                    if (playerOreWithWorker == i)
                        playerOreDiscount = i;
                    break;
                }
            if (player.Role == "Builder")//如果玩家是建築師可以再折扣一元
                playerIsBuilderDiscount = 1;

            int buildingCostAfterDis = (building.cost - playerOreDiscount - playerIsBuilderDiscount) < 0 ? 0 : building.cost - playerOreDiscount - playerIsBuilderDiscount;

            if (player.Money >= buildingCostAfterDis)
                return buildingCostAfterDis;

            return -1;

        }
    }
}

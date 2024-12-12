using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            if (player.FarmList.Count == 0)
                return -1;
            int playerOreWithWorker = player.FarmList.Where(x => x.Name == "Quarry  ").Where(x => x.worker > 0).ToList().Count;
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

        static public int CheckedHarvest(Player player, Goods good)//小於0代表無法收成
        {
            if (good.GetType() == typeof(Corn))
            {
               return player.GetFarmWorker("Corn");
            }
            else
            {
                int FarmWorker = player.GetFarmWorker(good.GetType().Name);
                if(FarmWorker <= 0)
                    return 0;
                int BuildingWorker = player.GetBuildingWorker(good.GetType().Name);
                return Math.Min(BuildingWorker, FarmWorker);
            }
        }
        //static public Dictionary<Type, Type> TransType = new Dictionary<Type, Type>();
        //static public void TransToBuildingType()
        //{
        //    TransType.Add(typeof(Coffee),typeof(CoffeeFarm));
        //    TransType.Add(typeof(Coffee), typeof(CoffeeFarm));
        //}
        static public bool CheckBuildingWithWorker(Player player, Type buildingType)
        {
            return player.GetAllBuildings().Where(x => x.GetType() == buildingType && x.worker > 0).ToList().Count > 0 ? true : false;
        }
    }
}

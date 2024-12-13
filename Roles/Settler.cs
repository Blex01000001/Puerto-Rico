using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    //選擇開拓者的玩家可以不拿郊區方塊嗎?
    //其他玩家可以不拿郊區方塊嗎?
    public class Settler : RoleAbstract //開拓者
    {
        public override string Name
        {
            get { return "Settler"; }
        }
        public override int Money
        {
            get { return _money; }
            set { _money = value; }
        }
        private int _money;
        public Settler()
        {
            _money = 0;
        }

        public override void Action(Player player, Game game)
        {
            //開拓者選擇一個打開的郊區方塊，或者拿一個採礦場（特權），放在自己的郊區空格上；接著，下家可以從剩下打開的郊區方塊中選擇一個，放在自己的郊區空格上；依此類推。
            Console.WriteLine($"\t{Name} Action");
            foreach (Player p1 in Player.GetPlayerListFromRole(player)) //從開拓者開始選農田，接著下家，依此類推
            {   
                bool ConstructionhutRules = Func.CheckBuildingWithWorker(p1, typeof(Constructionhut));
                if (game.availableFarms.Count <= 0)
                {
                    Console.WriteLine("***No available Farms***");
                    continue;
                }
                else if(p1.FarmList.Count >= 12)
                {
                    Console.WriteLine($"***{p1.Name} field is full***");
                    continue;
                }
                //開拓者可以選擇一個打開的郊區方塊，或者拿一個採礦場（特權）
                else if (p1.Role == "Settler" && game.quarryFields.Count > 0 && player.FarmList.Count < 12) 
                {
                    GetQuarry(p1, game);
                    continue;
                }
                //建築舍作用時，當開拓者出現時，玩家猶如開拓者一般地可以選擇拿採礦場（當然要採礦場還有剩的時候）或者普通的農田空格。
                else if (ConstructionhutRules && game.quarryFields.Count > 0 && player.FarmList.Count < 12)
                {
                    GetQuarry(p1, game);
                    continue;
                }
                GetField(p1, game);
                game.ArrangeFarms();
            }

            //if (game.quarryFields.Count > 0 && player.FarmList.Count < 12)//如果礦場不為零的話就拿礦場
            //{
            //    Console.WriteLine($"\t{player.Name} select the {game.quarryFields[0].Name}({game.quarryFields[0].GetHashCode()}");
            //    player.FarmList.Add(game.quarryFields[0]);
            //    game.quarryFields.Remove(game.quarryFields[0]);
            //}
            //else if (player.FarmList.Count < 12 && game.availableFarms.Count > 0)
            //{
            //    Console.WriteLine($"\t{player.Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
            //    player.FarmList.Add(game.availableFarms[0]);
            //    //player.BuildingList.Add(game.availableFarms[0]);
            //    game.availableFarms.Remove(game.availableFarms[0]);
            //}
            //else
            //{
            //    Console.WriteLine($"\t{player.Name} field is full");
            //}

            //int gamePlayerCount = Player.list.Count;
            //int playerIndex = Player.list.FindIndex(x => x == player);

            //for (int i = 1; i < gamePlayerCount; i++)//其他人輪流拿農田
            //{
            //    if (game.availableFarms.Count <= 0)
            //    {
            //        Console.WriteLine("***No available Farms***");
            //        continue;
            //    }
            //    int ii = i + playerIndex > gamePlayerCount - 1 ? i + playerIndex - gamePlayerCount : i + playerIndex;
            //    if (Player.list[ii].FarmList.Count >= 12)
            //    {
            //        Console.WriteLine($"***{Player.list[ii].Name} field is full***");
            //        continue;
            //    }
            //    Console.WriteLine($"\t{Player.list[ii].Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
            //    Player.list[ii].FarmList.Add(game.availableFarms[0]);
            //    //Player.list[ii].BuildingList.Add(game.availableFarms[0]);
            //    game.availableFarms.Remove(game.availableFarms[0]);
            //}

            //game.ArrangeFarms();

        }
        private void GetQuarry(Player p1, Game game)
        {
            Console.WriteLine($"\t{p1.Name} select the {game.quarryFields[0].Name}({game.quarryFields[0].GetHashCode()}");
            p1.FarmList.Add(game.quarryFields[0]);
            game.quarryFields.Remove(game.quarryFields[0]);
        }
        private void GetField(Player p1, Game game)
        {
            Console.WriteLine($"\t{p1.Name} select the {game.availableFarms[0].Name}({game.availableFarms[0].GetHashCode()})");
            p1.FarmList.Add(game.availableFarms[0]);
            game.availableFarms.Remove(game.availableFarms[0]);
        }
    }
}

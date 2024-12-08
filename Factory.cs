using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    //
    //工廠
    //
    internal class IndigoPlant : Building
    {
        public IndigoPlant(int type)
        {
            if (type == 0)
            {
                //Level1
                Name = "IndigoPlant_Small";
                Industry = "Indigo";
                worker = 0;
                cost = 1;
                MaxWorker = 1;
                Score = 1;
            }else if (type == 1)
            {
                //Level2
                Name = "IndigoPlant_Big";
                Industry = "Indigo";
                worker = 0;
                cost = 3;
                MaxWorker = 3;
                Score = 2;
            }
        }
    }
    internal class SugarMill : Building
    {
        public SugarMill(int type)
        {
            if (type == 0)
            {
                //Level1
                Name = "SugarMill_S";
                Industry = "Sugar";
                worker = 0;
                cost = 2;
                MaxWorker = 1;
                Score = 1;

            }
            else if (type == 1)
            {
                //Level2
                Name = "SugarMill_B";
                Industry = "Sugar";
                worker = 0;
                cost = 4;
                MaxWorker = 3;
                Score = 2;
            }

        }
    }
    internal class TobaccoStorage : Building
    {
        public TobaccoStorage()
        {
            //Level3
            Name = "TobaccoStorage";
            Industry = "Tobacco";
            worker = 0;
            cost = 5;
            MaxWorker = 3;
            Score = 3;
        }
    }
    internal class CoffeeRoaster : Building
    {
        public CoffeeRoaster()
        {
            //Level3
            Name = "CoffeeRoaster";
            Industry = "Coffee";
            worker = 0;
            cost = 6;
            MaxWorker = 2;
            Score = 3;
        }
    }
    //
    //建築物
    //
    internal class Smallmarket : Building
    {
        public Smallmarket(int type)
        {
            //Level3
            Name = "Smallmarket";
            Industry = "";
            worker = 0;
            cost = 1;
            MaxWorker = 1;
            //Score = 0;
        }
    }


    //
    //空建築，當作PASS
    //
    internal class PassBuilding : Building
    {
        public PassBuilding()
        {
            Name = "PassBuilding";
        }
    }
}

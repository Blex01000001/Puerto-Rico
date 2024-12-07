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
    internal class IndigoPlant_B : Building
    {
        public IndigoPlant_B()
        {
            //Level2
            Name = "IndigoPlant_B";
            worker = 0;
            cost = 3;
            MaxWorker = 3;
            Score = 2;
        }
    }
    internal class IndigoPlant_S : Building
    {
        public IndigoPlant_S()
        {
            //Level1
            Name = "IndigoPlant_S";
            worker = 0;
            cost = 1;
            MaxWorker = 1;
            Score = 1;
        }
    }
    internal class SugarMill_B : Building
    {
        public SugarMill_B()
        {
            //Level2
            Name = "SugarMill_B";
            worker = 0;
            cost = 4;
            MaxWorker = 3;
            Score = 2;
        }
    }
    internal class SugarMill_S : Building
    {
        public SugarMill_S()
        {
            //Level1
            Name = "SugarMill_S";
            worker = 0;
            cost = 2;
            MaxWorker = 1;
            Score = 1;
        }
    }
    internal class TobaccoStorage : Building
    {
        public TobaccoStorage()
        {
            //Level3
            Name = "TobaccoStorage";
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
        public Smallmarket()
        {
            //Level3
            Name = "CoffeeRoaster";
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

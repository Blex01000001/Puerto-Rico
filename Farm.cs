using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    abstract class Building
    {
        public string Name;
        public string Industry;
        public int worker;
        public int MaxWorker;
        public int cost;
        public int Score;
        public int Level;

    }
    internal class QuarryFarm : Building
    {
        public QuarryFarm()
        {
            Name = "Quarry  ";
            Industry = "Quarry";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class CoffeeFarm : Building
    {
        public CoffeeFarm()
        {
            Name = "Coffee  ";
            Industry = "Coffee";
            worker = 0; 
            MaxWorker = 1;
        }
    }
    internal class TobaccoFarm : Building
    {
        public TobaccoFarm()
        {
            Name = "Tobacco ";
            Industry = "Tobacco";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class CornFarm : Building
    {
        public CornFarm()
        {
            Name = "Corn    ";
            Industry = "Corn";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class SugarFarm : Building
    {
        public SugarFarm()
        {
            Name = "Sugar   ";
            Industry = "Sugar";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class IndigoFarm : Building
    {
        public IndigoFarm()
        {
            Name = "Indigo  ";
            Industry = "Indigo";
            worker = 0;
            MaxWorker = 1;
        }
    }

}

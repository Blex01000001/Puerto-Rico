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
        public int worker;
        public int MaxWorker;
        public int cost;
        public int Score;
        public int Level;

    }
    internal class Quarry : Building
    {
        public Quarry()
        {
            Name = "Quarry  ";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Coffee : Building
    {
        public Coffee()
        {
            Name = "Coffee  ";
            worker = 0; 
            MaxWorker = 1;
        }
    }
    internal class Tobacco : Building
    {
        public Tobacco()
        {
            Name = "Tobacco ";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Corn : Building
    {
        public Corn()
        {
            Name = "Corn    ";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Sugar : Building
    {
        public Sugar()
        {
            Name = "Sugar   ";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Indigo : Building
    {
        public Indigo()
        {
            Name = "Indigo  ";
            worker = 0;
            MaxWorker = 1;
        }
    }

}

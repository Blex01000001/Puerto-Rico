using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    abstract class Building
    {
        public int worker;
        public string Name;
        public int MaxWorker;
    }
    abstract class Farm : Building
    {
        
    }
    internal class Quarry : Farm
    {
        public Quarry()
        {
            Name = "Quarry";
            worker = 0;
            MaxWorker = 5;
        }
    }
    internal class Coffee : Farm
    {
        public Coffee()
        {
            Name = "Coffee";
            worker = 0; 
            MaxWorker = 1;
        }
    }
    internal class Tobacco : Farm
    {
        public Tobacco()
        {
            Name = "Tobaco";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Corn : Farm
    {
        public Corn()
        {
            Name = "Corn  ";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Sugar : Farm
    {
        public Sugar()
        {
            Name = "Sugar ";
            worker = 0;
            MaxWorker = 1;
        }
    }
    internal class Indigo : Farm
    {
        public Indigo()
        {
            Name = "Indigo";
            worker = 0;
            MaxWorker = 1;
        }
    }

}

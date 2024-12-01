using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    abstract class Farm
    {
        public string Name;
    }
    internal class Quarry : Farm
    {
        public Quarry()
        {
            Name = "Quarry";
        }
    }
    internal class Coffee : Farm
    {
        public Coffee()
        {
            Name = "Coffee";
        }
    }
    internal class Tobacco : Farm
    {
        public Tobacco()
        {
            Name = "Tobaco";
        }
    }
    internal class Corn : Farm
    {
        public Corn()
        {
            Name = "Corn";
        }
    }
    internal class Sugar : Farm
    {
        public Sugar()
        {
            Name = "Sugar";
        }
    }
    internal class Indigo : Farm
    {
        public Indigo()
        {
            Name = "Indigo";
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Field
    {
        public string Name { get; set; }
        public int Worker;
        //public int Quarry;
        //public int Coffee;
        //public int Tobacco;
        //public int Corn;
        //public int Sugar;
        //public int Indigo;

        public Field(string name)
        {
            this.Name = name;
            //Quarry = 8;
            //Coffee = 8;
            //Tobacco = 9;
            //Corn = 10;
            //Sugar = 11;
            //Indigo = 12;
        }
    }
}

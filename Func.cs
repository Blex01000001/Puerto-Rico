using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Puerto_Rico
{
    internal class Func
    {
        static public void shift<T>(T item, List<T> SourceList, List<T> TargetList)
        {
            TargetList.Add(item);
            SourceList.Remove(item);
        }
    }
}

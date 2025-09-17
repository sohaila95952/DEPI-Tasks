using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class Pair<T,U>
    {
        //this differ from pair<T> in : you cant define different type for each property
        public T First;
        public U Second;
        public Pair(T first,U second)
        {
            First = first;
            Second = second;
        }
        public override string ToString()
        {
            return $"{First} , {Second}";
        }
    }
}

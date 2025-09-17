using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class PhoneBook
    {
        Dictionary<string, int> _person = new Dictionary<string, int>();
        public PhoneBook(string name, int phone)
        {
            _person[name] = phone;
        }
        public int this [string name]
        { 
            get
            {
                return _person[name];
            }
        
            set
            {
                _person[name]=value;
            } 
         }
    }
}

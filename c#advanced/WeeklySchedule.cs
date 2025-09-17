using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class WeeklySchedule
    {
        Dictionary<string,List<string>> weeklySchedule;
        public WeeklySchedule()
        {
            weeklySchedule = new Dictionary<string, List<string>>()
            { ["saturday"] = new List<string>(),
                ["sunday"] = new List<string>(),
                ["monday"] = new List<string>() ,
                ["tuesday"] = new List<string>(),
                ["wednesday"] = new List<string>(),
                ["thursday"] = new List<string>(),
                ["friday"] = new List<string>()
            };
            
        }
        public List<string> this [string day]{
            get
            {
                return weeklySchedule[day];
            }
            set 
            { 
                weeklySchedule[day] = value;
            }
            }
        public void print(string day)
        {
            Console.WriteLine(day+" : ");
            foreach (var item in weeklySchedule[day]) {
                Console.WriteLine($" {item}");
            }

        }
    }
}

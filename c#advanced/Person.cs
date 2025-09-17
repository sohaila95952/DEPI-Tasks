using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal class Person
    {
        public Person(string? middleName, DateTime? dateOfBirth)
        {
            MiddleName = middleName;
            DateOfBirth = dateOfBirth;
        }

        public string? MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public override string ToString()
        {
            string middle = MiddleName ?? "No Middle Name";
            string dob = DateOfBirth?.ToString("dd-mm-yyyy") ?? "UnKnown";
            return $"Middle Name : {middle} , Date Of Birth : {dob}";
        }
    }
}

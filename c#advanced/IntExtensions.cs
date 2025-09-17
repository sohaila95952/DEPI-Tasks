using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal static class IntExtensions
    {
        public static bool IsEven(this int number) => number % 2 == 0;
        public static bool IsOdd(this int number) => number % 2 != 0;

        public static bool IsPrime(this int number)
        {
            if (number < 2) return false;
            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
        public static string ToRoman(this int number)
        {
            if (number <= 0) return string.Empty;
            Dictionary<int, string> romanMap = new Dictionary<int, string>()
        {//ordered
            {1000, "M"},
            {900, "CM"},
            {500, "D"},
            {400, "CD"},
            {100, "C"},
            {90, "XC"},
            {50, "L"},
            {40, "XL"},
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"}
        };
            string result = "";
            foreach (var kvp in romanMap)
            {
                while (number >= kvp.Key)
                {
                    result += kvp.Value;  
                    number -= kvp.Key; 
                }
            }

            return result;
        }
        public static long Factorial(this int number)
        {
            if (number < 0)
                throw new ArgumentException("Factorial is not defined for negative numbers.");

            long result = 1;

            for (int i = 2; i <= number; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}


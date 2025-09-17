using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c_advanced
{
    internal static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime date,DayOfWeek dayOfWeek=DayOfWeek.Saturday)
        {

            int diff = (7 + (date.DayOfWeek - dayOfWeek)) % 7;
            return date.AddDays(-diff).Date;
        }
        public static DateTime EndOfWeek(this DateTime date, DayOfWeek dayOfWeek = DayOfWeek.Saturday)
        {
            return date.StartOfWeek(dayOfWeek).AddDays(6);
        }
        public static DateTime StartOfMonth(this DateTime date) {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month,DateTime.DaysInMonth(date.Year,date.Month));
        }
        public static DateTime StartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }
        public static DateTime EndOfYear(this DateTime date)
        {
            return new DateTime(date.Year,12, 31);
        }
        public static int CalculateAge(this DateTime date) {
            var today = DateTime.Now;
            var age=today.Year-date.Year;
           if (today < date.AddYears(age))
                age--;
            return age;
        }
        public static bool isBusinessDay(this DateTime date) {
            return date.DayOfWeek != DayOfWeek.Friday|| date.DayOfWeek != DayOfWeek.Saturday;
        }

    }
}

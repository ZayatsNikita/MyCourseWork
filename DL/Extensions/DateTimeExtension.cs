using System;

namespace DL.Extensions
{
    public static class DateTimeExtension
    {
        public static String GetDateInString(this DateTime? value)
        {
            int year = value.Value.Year;
            int month = value.Value.Month;
            int day = value.Value.Day;

            return $"\'{day}.{month}.{year}\'";
        }
    }
}

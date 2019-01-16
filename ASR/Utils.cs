using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ASR
{
    public static class Utils
    {
        private static readonly string dateFormat;
        private static readonly string timeFormat;
        static Utils()
        {
            dateFormat = "dd-MM-yyyy";
            timeFormat = "HH:mm:ss";
        }
        public static bool ValidateDate(string date)
        {
            try
            {
                DateTime.ParseExact(date, dateFormat, DateTimeFormatInfo.InvariantInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool ValidateTime(string time)
        {
            try
            {
                DateTime.ParseExact($"{time}:00", timeFormat, DateTimeFormatInfo.InvariantInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

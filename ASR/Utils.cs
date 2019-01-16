using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ASR
{
    public static class Utils
    {
        public static bool ValidateDate(string date)
        {
            try
            {
                DateTime.ParseExact(date, "dd-MM-yyyy", DateTimeFormatInfo.InvariantInfo);
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
                DateTime.ParseExact($"{time}:00", "HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

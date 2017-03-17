using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace DM.Common
{
    public static class DateTimeHelper
    {
        public static string GetDateString(DateTime ptime)
        {
            return ptime.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        }

        public static string ConvertToMonth(string pDate)
        {
            if (string.IsNullOrEmpty(pDate))
            {
                return pDate;
            }
            DateTime dateTime = Convert.ToDateTime(pDate);
            return string.Format("{0}-{1}", dateTime.Year.ToString(), dateTime.Month.ToString().PadLeft(2, '0'));
        }

        public static string ConvertToDay(string pDate)
        {
            if (string.IsNullOrEmpty(pDate))
            {
                return pDate;
            }
            DateTime dateTime = Convert.ToDateTime(pDate);
            return string.Format("{0}-{1}-{2}", dateTime.Year.ToString(), dateTime.Month.ToString().PadLeft(2, '0'), dateTime.Day.ToString().PadLeft(2, '0'));
        }
    }
}

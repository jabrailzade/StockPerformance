using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetFormattedDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy'-'MM'-'dd");
        }

        public static string GetPreviousWeekStart(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy'-'MM'-'dd");
        }

        public static string GetPreviousWeekEnd(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy'-'MM'-'dd");
        }
    }
}

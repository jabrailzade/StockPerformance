using System;

namespace WebApi.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime _epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

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

        public static double DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            return (TimeZoneInfo.ConvertTimeToUtc(dateTime) -_epochDateTime).TotalMilliseconds;
        }

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            var result = _epochDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return result;
        }
    }
}

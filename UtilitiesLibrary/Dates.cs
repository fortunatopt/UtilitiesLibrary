using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitiesLibrary
{
    public static class DateExtensions
    {
        private static DateTime GetEpoch()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> kind.
        /// </summary>
        public static DateTime ToDateTimeFromEpoch(this long intDate) => GetEpoch().AddMilliseconds(intDate);

        /// <summary>
        /// Converts the given DateTime to a Unix Timestamp
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToUnixTimestamp(this DateTime dateTime) => Math.Round(dateTime.Subtract(GetEpoch()).TotalMilliseconds).ToString();

        public static DateTime UnixTimeStampToDateTime(this string unixTimeStampString)
        {
            // Unix timestamp is seconds past epoch
            double unixTimeStamp = double.Parse(unixTimeStampString);
            System.DateTime dtDateTime = GetEpoch();
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static List<DateTime> SortAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static List<DateTime> SortDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.CompareTo(a));
            return list;
        }

        public static List<DateTime> SortMonthAscending(this List<DateTime> list)
        {
            list.Sort((a, b) => a.Month.CompareTo(b.Month));
            return list;
        }

        public static List<DateTime> SortMonthDescending(this List<DateTime> list)
        {
            list.Sort((a, b) => b.Month.CompareTo(a.Month));
            return list;
        }
    }
}

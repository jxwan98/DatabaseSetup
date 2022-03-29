using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseSetup.Helpers
{
    public static class TimeHelper
    {
        public static uint CurrentTimestamp
        {
            get
            {
                return (uint)DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }
        public static uint CurrentUTCTimestamp
        {
            get
            {
                return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }

        public static uint DaysToSeconds(int days)
        {
            return (uint)days * 86400;
        }

        public static DateTime TimestampToDateTime(uint timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(timestamp);

            return dateTime;
        }

        public static uint DateTimeToTimestamp(DateTime dateTime)
        {
            uint timestamp = (uint)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return timestamp;
        }

        public static DateTime ConvertToUTCFromMalaysiaTimezone(DateTime UTC)
        {
            return UTC.AddHours(-8);
        }
    }
}

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
                return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }

        public static uint DaysToSeconds(int days)
        {
            return (uint)days * 86400;
        }

        public static DateTime TimestampToDateTime(uint timestamp, DateTimeKind timezone)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, timezone);
            var dateTime = epochDateTime.AddSeconds(timestamp);
            return dateTime;
        }

        public static uint DateTimeToTimestamp(DateTime dateTime)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            uint timestamp = (uint)dateTime.ToUniversalTime().Subtract(epochDateTime).TotalSeconds;
            return timestamp;
        }
    }
}

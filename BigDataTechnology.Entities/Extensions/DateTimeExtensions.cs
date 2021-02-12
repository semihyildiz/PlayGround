using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.Entities.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// converts epoch time to datetime
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime ConvertDateTime(this long time)
        {
            return DateTimeOffset.FromUnixTimeSeconds(time).DateTime; ;
        }
    }
}

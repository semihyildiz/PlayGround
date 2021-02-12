using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.Entities.Models.IntegrationModels
{
    public class DarkSKY
    {
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public string timezone { get; set; }
        public Currently currently { get; set; }
        public Daily daily { get; set; }


        public class Currently
        {
            //public DateTime DateTime
            //{
            //    get
            //    {
            //       return DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
            //    }
            //}

            public long time { get; set; }
            public string summary { get; set; }
            public double temperature { get; set; }
            public double apparentTemperature { get; set; }
            public string humidity { get; set; }
        }
        public class Daily
        {
            public string summary { get; set; }
            public List<Data> data { get; set; }
            public class Data
            {
                //public DateTime DateTime
                //{
                //    get
                //    {
                //        return DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
                //    }
                //}
                //public DateTime DateTimetemperatureHighTime
                //{
                //    get
                //    {
                //        return DateTimeOffset.FromUnixTimeSeconds(temperatureHighTime).DateTime;
                //    }
                //}
                //public DateTime DateTimetemperatureLowTime
                //{
                //    get
                //    {
                //        return DateTimeOffset.FromUnixTimeSeconds(temperatureLowTime).DateTime;
                //    }
                //}

                public long time { get; set; }
                public double temperatureHigh { get; set; }
                public double temperatureLow { get; set; }
                public long temperatureHighTime { get; set; }
                public long temperatureLowTime { get; set; }
            }
        }
    }

}

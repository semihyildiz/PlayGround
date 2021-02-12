using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.Entities.Models.Entities
{
    public class WeatherForecast
    {
        public int Id {get;set;}
        public DateTime RecordDate { get; set; }
        public string Location { get; set; }
        public  DateTime CurrentDateTime { get; set; }
        public double CurrentTemprature { get; set; }
        public DateTime HighestDateTimeInThisWeek { get; set; }
        public double HighestTempratureInThisWeek { get; set; }
        public double LowestTempratureInThisWeek { get; set; }
        public DateTime LowestDateTimeInThisWeek { get; set; }
        

    }
}

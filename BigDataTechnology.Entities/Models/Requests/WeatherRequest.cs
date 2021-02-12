using BigDataTechnology.Entities.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.Entities.Models.Requests
{
   public class WeatherRequest
    {
    
        public WeatherRequest()
        {
            this.WeatherForecast = new WeatherForecast();
        }
        public string Location { get; set; }
        public WeatherForecast WeatherForecast { get; set; }
        public Nullable<DateTime> StartDate { get; set; }
    }
}

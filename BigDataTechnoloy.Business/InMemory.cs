using BigDataTechnology.Entities.Models.Entities;
using BigDataTechnology.Entities.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigDataTechnoloy.Business
{
   public static class InMemory
    {
        private static List<WeatherForecast> DataList { get; set; }
        public static void Initial()
        {
            DataList = new List<WeatherForecast>();
        }

        public static void Add(WeatherForecast data)
        {
            lock (DataList)
            {
                DataList.Add(data);
            }
        }
        public static WeatherForecast  Get(WeatherRequest request)
        {
            lock (DataList)
            {
              return  DataList.Where(c => c.Location == request.Location).OrderByDescending(c => c.RecordDate).FirstOrDefault();
            }
        }
        public static void Flush()
        {
            lock (DataList)
            {
                DataList.RemoveAll(c => c.RecordDate < DateTime.Now.AddHours(-1));/*1 saat öncekilerin silinmesi*/
                DataList = DataList.OrderByDescending(c => c.RecordDate).Take(50).ToList();
            }
        }
    }
}

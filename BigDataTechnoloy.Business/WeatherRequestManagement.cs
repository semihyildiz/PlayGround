using BigDataTechnology.Entities;
using BigDataTechnology.Entities.Extensions;
using BigDataTechnology.Entities.Models.Requests;
using BigDataTechnology.Entities.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using BigDataTechnology.DAL;
using BigDataTechnology.DAL.Abstract;

namespace BigDataTechnoloy.Business
{
    public class WeatherRequestManagement
    {
        private readonly IServiceProvider _serviceProvider;
        public Worker worker { get; set; }
        public WeatherRequestManagement()
        {
            //IServiceCollection services = new ServiceCollection();
            //var DbContextDIService = services.BuildServiceProvider();
            //worker =(Worker) DbContextDIService.GetService(typeof(IWorker));
            worker = new Worker();
        }
        public Result<WeatherForecast> GetWeatherInformation(WeatherRequest request)
        {
            Result<WeatherForecast> result = new Result<WeatherForecast>();
            try
            {
                /*Cashde Ara varsa dön*/



                LocationIQManagement locationMan = new LocationIQManagement();//DI ekle. 
                var locations = locationMan.FindLocation(request.Location);

                if (locations.Count == 0)
                {
                    result.ResultCode = enResultCodes.NoContent;
                    return result;
                }

                DarkSKYManagement DarkSKYMan = new DarkSKYManagement();/*DI ekle*/
                var darkSky = DarkSKYMan.GetWeatherInfo(locations.FirstOrDefault().lat, locations.FirstOrDefault().lon);

                if (darkSky.daily.data.Count == 0)
                {
                    result.ResultCode = enResultCodes.DataCouldNotFound;
                    return result;
                }


                WeatherForecast forecast = new WeatherForecast();
                forecast.Location = request.Location;
                forecast.CurrentDateTime = darkSky.currently.time.ConvertDateTime();
                forecast.CurrentTemprature = darkSky.currently.temperature;

                var highest = darkSky.daily.data.OrderByDescending(c => c.temperatureHigh).FirstOrDefault();
                var lowest = darkSky.daily.data.OrderByDescending(c => c.temperatureLow).FirstOrDefault();

                forecast.HighestDateTimeInThisWeek = highest.temperatureHighTime.ConvertDateTime();
                forecast.HighestTempratureInThisWeek = highest.temperatureHigh;
                forecast.LowestDateTimeInThisWeek = highest.temperatureLowTime.ConvertDateTime();
                forecast.LowestTempratureInThisWeek = highest.temperatureLow;

                /*dbye git*/
                worker.WeatherForecast.Add(forecast);
                worker.SaveChanges();


                result.Object = forecast;
                result.ResultCode = enResultCodes.OK;
            }
            catch (Exception ex)
            {//TODO Loglanacak

                result.ResultCode = enResultCodes.Failed;
            }
            
            return result;
        }
    }
}

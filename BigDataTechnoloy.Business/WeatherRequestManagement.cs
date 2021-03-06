﻿using BigDataTechnology.Entities;
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
using System.Threading.Tasks;
using BigDataTechnoloy.Business.Integrations;

namespace BigDataTechnoloy.Business
{
    public class WeatherRequestManagement
    {
        //private readonly IServiceProvider _serviceProvider;
        public Worker worker { get; set; }
        public WeatherRequestManagement(Worker _worker)
        {
            worker = _worker;
        }
        public Result<WeatherForecast> GetWeatherInformation(WeatherRequest request)
        {
            Result<WeatherForecast> result = new Result<WeatherForecast>();
            try
            {
                if (string.IsNullOrEmpty(request.Location))
                {
                    result.ResultCode = enResultCodes.EmptyRequest;
                    return result;
                }

                /*Memory arama işlemi*/
                var InMemoryRecord = InMemory.Get(request);

                if (InMemoryRecord != null)
                {
                    result.Object = InMemoryRecord;
                    result.ResultCode = enResultCodes.OK;
                    return result;
                }

                /*dbde arama işlemi(son 24 saatte)*/
                request.StartDate = DateTime.Now.AddHours(-24);

                var existingDbRecord = worker.WeatherForecast.GetWeatherForecast(request).OrderByDescending(c => c.RecordDate).FirstOrDefault();

                if (existingDbRecord != null)
                {
                    InMemory.Add(existingDbRecord);
                    result.Object = existingDbRecord;
                    result.ResultCode = enResultCodes.OK;
                    return result;
                }

                /*3. parti servislerden sorgulama*/
                LocationIQManagement locationMan = new LocationIQManagement();//TODO DI ekle. 
                var locations = locationMan.FindLocation(request.Location);

                if (locations.Count == 0)
                {
                    result.ResultCode = enResultCodes.NoContent;
                    return result;
                }

                DarkSKYManagement DarkSKYMan = new DarkSKYManagement();/*TODO DI ekle*/
                var darkSky = DarkSKYMan.GetWeatherInfo(locations.FirstOrDefault().lat, locations.FirstOrDefault().lon);

                if (darkSky.daily.data.Count == 0)
                {
                    result.ResultCode = enResultCodes.DataCouldNotFound;
                    return result;
                }


                /*Kurumsal Nesnelerimize çevirme*/
                WeatherForecast forecast = new WeatherForecast();
                forecast.Location = request.Location;
                forecast.RecordDate = DateTime.Now;
                forecast.CurrentDateTime = darkSky.currently.time.ConvertDateTime();
                forecast.CurrentTemprature = darkSky.currently.temperature;

                var highest = darkSky.daily.data.OrderByDescending(c => c.temperatureHigh).FirstOrDefault();
                var lowest = darkSky.daily.data.OrderByDescending(c => c.temperatureLow).FirstOrDefault();

                forecast.HighestDateTimeInThisWeek = highest.temperatureHighTime.ConvertDateTime();
                forecast.HighestTempratureInThisWeek = highest.temperatureHigh;
                forecast.LowestDateTimeInThisWeek = highest.temperatureLowTime.ConvertDateTime();
                forecast.LowestTempratureInThisWeek = highest.temperatureLow;
                

                Task.Factory.StartNew(() =>
                {/*dbye git*/
                    forecast=worker.WeatherForecast.Add(forecast);
                    worker.SaveChanges();
                    InMemory.Add(forecast);
                });


                result.Object = forecast;
                //result.MiddlewareData = "newQuery";
                result.ResultCode = enResultCodes.OK;
            }
            catch (Exception ex)
            {//TODO Loglanacak

                result.ResultCode = enResultCodes.Failed;
            }

            return result;
        }
        public Result<List<WeatherForecast>> GetWeatherInformations(WeatherRequest request)
        {
            Result<List<WeatherForecast>> result = new Result<List<WeatherForecast>>();
            try
            {
                var existingDbRecord = worker.WeatherForecast.GetWeatherForecast(request).OrderByDescending(c => c.RecordDate).ToList();

                result.Object = existingDbRecord;
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

using BigDataTechnology.Entities.Models.Entities;
using BigDataTechnology.Entities.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigDataTechnology.DAL.Abstract
{
    public interface IWeatherForecast : IRepository<WeatherForecast>
    {
        IQueryable<WeatherForecast> GetWeatherForecast(WeatherRequest request);
    }
}

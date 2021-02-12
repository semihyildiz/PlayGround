using BigDataTechnology.DAL.Abstract;
using BigDataTechnology.Entities.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.DAL.Repositories
{
  public  class RepWeatherForecast : RepGeneric<DbContext, WeatherForecast>, IWeatherForecast
    {
        public RepWeatherForecast(DbContext dbContext) : base(dbContext)
        {
            
        }
    }
}

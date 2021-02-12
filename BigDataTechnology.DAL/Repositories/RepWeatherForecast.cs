using BigDataTechnology.DAL.Abstract;
using BigDataTechnology.Entities.Models.Entities;
using BigDataTechnology.Entities.Models.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigDataTechnology.DAL.Repositories
{
  public  class RepWeatherForecast : RepGeneric<DbContext, WeatherForecast>, IWeatherForecast
    {
        public RepWeatherForecast(DbContext dbContext) : base(dbContext)
        {
            
        }

        public IQueryable<WeatherForecast> GetWeatherForecast(WeatherRequest request)
        {
            var Query = DbContext.Set<WeatherForecast>().AsQueryable();
            if (string.IsNullOrEmpty(request.Location) == false)
                Query = Query.Where(c => c.Location == request.Location);
            if (request.StartDate != null)
                Query = Query.Where(c => c.RecordDate > request.StartDate);

            return Query;
        }
    }
}

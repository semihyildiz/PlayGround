using BigDataTechnology.Entities.Models.Entities;
using BigDataTechnoloy.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigDataTechnology.Entities.Extensions;
using System.Net.Http;
using System.Net;
using BigDataTechnology.Entities;
using BigDataTechnology.Entities.Models.Requests;

namespace BigDataTechnology.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        #region Old
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //private readonly ILogger<WeatherForecastController> _logger;

        //public WeatherForecastController(ILogger<WeatherForecastController> logger)
        //{
        //    _logger = logger;
        //}

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get(string locationName)
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //} 
        #endregion


        [HttpGet]
        public Result<WeatherForecast> Get(string location)
        {
            WeatherRequestManagement weatherMan = new WeatherRequestManagement();
            WeatherRequest request = new WeatherRequest();
            request.Location = location;
            var result= weatherMan.GetWeatherInformation(request);
            /*SignalR notification*/

            return result;
        }
    }
}

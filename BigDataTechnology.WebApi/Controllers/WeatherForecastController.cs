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
using BigDataTechnoloy.Business.Hubs;
using System.Text.Json;
using BigDataTechnology.DAL.Abstract;
using BigDataTechnology.DAL;

namespace BigDataTechnology.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly Worker _Worker;
        public WeatherForecastController(Worker worker)
        {
            _Worker = worker;
        }

        [HttpGet]
        public Result<WeatherForecast> Get(string location)
        {
            WeatherRequest request = new WeatherRequest();
            request.Location = location;
            WeatherRequestManagement weatherMan = new WeatherRequestManagement(_Worker);
            var result = weatherMan.GetWeatherInformation(request);

            return result;
        }
    }
}

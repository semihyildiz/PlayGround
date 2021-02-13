using BigDataTechnology.DAL;
using BigDataTechnology.Entities;
using BigDataTechnology.Entities.Models.Entities;
using BigDataTechnology.Entities.Models.Requests;
using BigDataTechnoloy.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigDataTechnology.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitoringController : Controller
    {
        private readonly Worker _Worker;
        public MonitoringController( Worker worker)
        {
            _Worker = worker;
        }
        [HttpGet]
        public Result<List<WeatherForecast>> Get()
        {
            WeatherRequestManagement weatherMan = new WeatherRequestManagement(_Worker);
            WeatherRequest request = new WeatherRequest();
            request.StartDate = DateTime.Now.AddHours(-24);
            var result = weatherMan.GetWeatherInformations(request);
            return result;
        }
    }
}

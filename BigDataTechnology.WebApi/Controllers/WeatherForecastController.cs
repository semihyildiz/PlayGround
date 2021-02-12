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

        public WeatherForecastController(IChatHubDispatcher chatHubDispatcher, Worker worker)
        {
            _chatHubDispatcher = chatHubDispatcher;
            _Worker = worker;
        }

        private readonly Worker _Worker;
        private readonly IChatHubDispatcher _chatHubDispatcher;
        [HttpGet]
        public Result<WeatherForecast> Get(string location)
        {
            string TranscationId = Guid.NewGuid().ToString();

            Task.Factory.StartNew(() =>
            {
                _chatHubDispatcher.SendAllClients(TranscationId + DateTime.Now.ToString("dd:MM:yyyy HH:MM:ss:fff") + " Request=", location);
            });

            WeatherRequestManagement weatherMan = new WeatherRequestManagement(_Worker);
            WeatherRequest request = new WeatherRequest();
            request.Location = location;
            var result = weatherMan.GetWeatherInformation(request);

            /*SignalR notification*/
            _chatHubDispatcher.SendAllClients(TranscationId + DateTime.Now.ToString("dd:MM:yyyy HH:MM:ss:fff") + " Response=", JsonSerializer.Serialize(result));


            /*middlewaree almak lazım*/
            Task.Factory.StartNew(() =>
            {
                InMemory.Flush();
            });

            return result;
        }
    }
}

using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using Microsoft.AspNet.SignalR.Client;

namespace BigDataTechnology.Monitoring
{
    class Program
    {
        static string _baseurl = "http://localhost:58023/";
        static void Main(string[] args)
        {
            Console.WriteLine("Wellcome BigDataTechnology Monitoring Tool ");

            getDailyrecordedsearches();

            Connect();
            Console.ReadKey();
        }
        static void getDailyrecordedsearches()
        {
            Console.WriteLine("Daily recorded searches");
            RestClient Client = new RestClient(_baseurl);
            string subUrl = "Monitoring";
            RestRequest restRequest = new RestRequest(subUrl);
            restRequest.Method = Method.GET;
            var httpResult = Client.Execute(restRequest);
            if (httpResult.StatusCode == System.Net.HttpStatusCode.OK) 
            {
                var result = JsonConvert.DeserializeObject<Result<List<WeatherForecast>>>(httpResult.Content);
                if ( result.Object.Count > 0)
                {
                    foreach (var item in result.Object)
                    {
                        Console.WriteLine(string.Format("Location:{0}, RecordDate:{1}, Current Temprature:{2}, Highest Temprature in this Week:{3}, Lowest Temprature in this Week :{4}", item.Location, item.RecordDate.ToString("dd:MM:yyyy HH:MM"),item.CurrentTemprature,item.HighestTempratureInThisWeek,item.LowestTempratureInThisWeek));
                    }
                }
                else
                {
                    Console.WriteLine("Could not found any record");
                }
            }
          

        }
        static async Task Connect()
        {
            try
            {
                HubConnection connection = new HubConnectionBuilder().WithUrl(_baseurl + "chathub").Build();
                await connection.StartAsync();

                Console.WriteLine("Connected to the Hub on " + _baseurl + "chathub");

                connection.InvokeAsync("TestOk", System.DateTime.Now.ToString());

                connection.On("ReceiveMessage", (string server, string message) =>
                {
                    Console.WriteLine($"{server}: {message}");
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection Error Occurs=> url: " + _baseurl + "chathub");
                Console.WriteLine("Press any key for exit");
            }

        }
       
    }
}

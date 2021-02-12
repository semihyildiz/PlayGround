using BigDataTechnology.Entities.Models.IntegrationModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace BigDataTechnoloy.Business
{
    /// <summary>
    /// https://api.darksky.net/forecast/[key]/[latitude],[longitude]
    /// </summary>
    public class DarkSKYManagement//interfacee alınacak
    {
        public string _baseUrl = "https://api.darksky.net/";
        public string _key = "f3146e0fc78b4930d41a60703c08e2ae";

        public DarkSKYManagement()
        {
            Client = new RestClient(_baseUrl);
        }
        private RestClient Client { get; set; }
        public DarkSKY GetWeatherInfo( double latitude, double longitude)
        {
            DarkSKY result = new DarkSKY();
            try
            {
                string subUrl = string.Format("forecast/{2}/{0},{1}", latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture), _key);
                RestRequest restRequest = new RestRequest(subUrl);
                restRequest.Method = Method.GET;
                var httpResult = Client.Execute(restRequest);
                result = JsonConvert.DeserializeObject<DarkSKY>(httpResult.Content);
            }
            catch (Exception ex)
            {/*TODO Loglama yapılacak*/

            }
            return result;
        }
    }
}

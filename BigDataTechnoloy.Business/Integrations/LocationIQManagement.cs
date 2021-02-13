using BigDataTechnology.Entities.Models.IntegrationModels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace BigDataTechnoloy.Business.Integrations
{
    public class LocationIQManagement
    {
        public string _baseUrl = "https://eu1.locationiq.com/v1/";
        public string _token = "a1779b7817b3b2";
  
        public LocationIQManagement()
        {
            Client = new RestClient(_baseUrl);
        }
        private RestClient Client { get; set; }
        public List<LocationIQ> FindLocation(string location)
        {
            List<LocationIQ> result = new List<LocationIQ>();
            try
            {
                string subUrl = string.Format("search.php?key={1}&q=[{0}]&format=json", location, _token);
                RestRequest restRequest = new RestRequest(subUrl);
                restRequest.Method = Method.GET;
                var httpResult = Client.Execute(restRequest);
                result = JsonConvert.DeserializeObject<List<LocationIQ>>(httpResult.Content);
            }
            catch (Exception ex)
            {/*TODO Loglama yapılacak*/

            }
            return result;
        }
    }
}

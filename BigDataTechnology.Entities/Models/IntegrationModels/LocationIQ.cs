using System;
using System.Collections.Generic;
using System.Text;

namespace BigDataTechnology.Entities.Models.IntegrationModels
{
    public class LocationIQ
    {
        public string place_id { get; set; }
        public string licence { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string display_name { get; set; }
    }
}

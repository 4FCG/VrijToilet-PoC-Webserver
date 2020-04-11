using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webserver_PoC.Models
{
    public class ApiSensor
    {
        public int sensor_id { get; set; }

        public string name { get; set; }

        public string location_description { get; set; }

        public decimal longitude { get; set; }

        public decimal latitude { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webserver_PoC.Models
{
    public class ApiMeting
    {
        public int meting_id { get; set; }

        public DateTime received_timestamp { get; set; }

        public int meting_count { get; set; }

        public int sensor_id { get; set; }
    }
}
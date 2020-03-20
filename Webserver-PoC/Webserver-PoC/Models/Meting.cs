namespace Webserver_PoC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Meting")]
    public partial class Meting
    {
        [Key]
        public int meting_id { get; set; }

        public DateTime received_timestamp { get; set; }

        public int meting_count { get; set; }

        public int sensor_id { get; set; }

        public virtual Sensor Sensor { get; set; }
    }
}

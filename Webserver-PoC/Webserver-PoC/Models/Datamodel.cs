namespace Webserver_PoC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Datamodel : DbContext
    {
        public Datamodel()
            : base("name=Datamodel")
        {
        }

        public virtual DbSet<Meting> Metings { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sensor>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.location_description)
                .IsUnicode(false);
        }
    }
}

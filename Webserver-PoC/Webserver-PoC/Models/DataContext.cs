namespace Webserver_PoC.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DataContext")
        {
        }

        public virtual DbSet<Beheerder> Beheerders { get; set; }
        public virtual DbSet<Meting> Metings { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beheerder>()
                .Property(e => e.voornaam)
                .IsUnicode(false);

            modelBuilder.Entity<Beheerder>()
                .Property(e => e.achternaam)
                .IsUnicode(false);

            modelBuilder.Entity<Beheerder>()
                .Property(e => e.gebruikersnaam)
                .IsUnicode(false);

            modelBuilder.Entity<Beheerder>()
                .Property(e => e.wachtwoord)
                .IsUnicode(false);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.location_description)
                .IsUnicode(false);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.longitude)
                .HasPrecision(9, 6);

            modelBuilder.Entity<Sensor>()
                .Property(e => e.latitude)
                .HasPrecision(9, 6);
        }
    }
}

namespace Acme.RemoteFlights.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class FlightDbContext : DbContext
    {
        public FlightDbContext()
            : base("name=FlightDbContext")
        {
        }
        public FlightDbContext(string connectionString)    :base(connectionString)      
        {
        }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .Property(e => e.CityName)
                .IsUnicode(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Flights)
                .WithRequired(e => e.City)
                .HasForeignKey(e => e.ArrivalCityId)                
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<City>()
                .HasMany(e => e.Flights1)
                .WithRequired(e => e.City1)
                .HasForeignKey(e => e.DepartingCityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Flight>()
                .Property(e => e.FlightName)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()                
                .Property(e => e.FirstName)                
                .IsUnicode(false);

         

            modelBuilder.Entity<Passenger>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Passenger>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);
        }
    }
}

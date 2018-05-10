namespace Acme.RemoteFlights.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Flight")]
    public partial class Flight
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flight()
        {
            Bookings = new HashSet<Booking>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string FlightName { get; set; }

        public string FlightNo { get; set; }

        public TimeSpan FlightBoardingTime { get; set; }

        public TimeSpan FlightArrivalTime { get; set; }

        public int? PassengerCapacity { get; set; }

        public int DepartingCityId { get; set; }

        public int ArrivalCityId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual City City { get; set; }

        public virtual City City1 { get; set; }
    }
}

namespace Acme.RemoteFlights.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? PassengerId { get; set; }

        public int? FlightId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TravelDay { get; set; }

        public virtual Flight Flight { get; set; }

        public virtual Passenger Passenger { get; set; }
    }
}

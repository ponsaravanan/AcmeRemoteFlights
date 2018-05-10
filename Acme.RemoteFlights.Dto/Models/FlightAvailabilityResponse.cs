using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.RemoteFlights.Dto.Models
{
    public class FlightAvailabilityResponse
    {
        public string FlightName { get; set; }
        public DateTime? TravelDay { get; set; }
        public int AvailableSeats { get; set; }
    }
}

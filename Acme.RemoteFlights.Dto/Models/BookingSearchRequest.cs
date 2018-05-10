using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.RemoteFlights.Dto.Models
{
    public class BookingSearchRequest
    {
        public string PassengerName { get; set; }
        public DateTime? TravelDate { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureCity { get; set; }
        public string FlightNumber { get; set; }
    }
}

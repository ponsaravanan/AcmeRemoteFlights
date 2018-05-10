using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.RemoteFlights.Dto.Models
{
    public class BookingSearchResponse
    {
        public string FlightName { get; set; }
        public string FlightNo { get; set; }
        public string PassengerFirstName { get; set; }
        public string PassengerLastName { get; set; }        
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.RemoteFlights.Dto.Models
{
    public class FlightAvailabiltyRequest
    {
        public string CityFrom { get; set; }
        public string CityTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Pax { get; set; }
    }
}

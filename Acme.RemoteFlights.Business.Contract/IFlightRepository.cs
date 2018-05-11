using Acme.RemoteFlights.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.RemoteFlights.Business.Contracts
{
    public interface IFlightRepository
    {
       IEnumerable<FlightDTO> List();
        bool CheckAvailability(FlightAvailabiltyRequest req);
        IEnumerable<FlightAvailabilityResponse> ListAvailability(FlightAvailabiltyRequest req);
    }
}

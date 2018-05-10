using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.RemoteFlights.Business.Contracts;
using Acme.RemoteFlights.Data;
using Acme.RemoteFlights.Dto.Models;
using Acme.RemoteFlights.Dto.Models.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Acme.RemoteFlights.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private IFlightRepository _flightRep;
        public FlightsController(IFlightRepository flightRep)
        {
            _flightRep = flightRep;
        }
        // GET api/values
        [HttpGet("list-flights")]
        public IEnumerable<FlightDTO> List() => _flightRep.List();

        [HttpPost("list-availability")]
        public IEnumerable<FlightAvailabilityResponse> ListAvailablity([FromBody] FlightAvailabiltyRequest req) => _flightRep.CheckAvailability(req);


    }
}

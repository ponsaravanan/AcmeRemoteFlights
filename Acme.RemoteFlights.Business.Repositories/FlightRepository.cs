using Acme.RemoteFlights.Business.Contracts;
using Acme.RemoteFlights.Data;
using Acme.RemoteFlights.Dto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.RemoteFlights.Business.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        FlightDbContext _dbCtx;
        public FlightRepository(FlightDbContext dbContext)
        {
            _dbCtx = dbContext;
        }

        public bool CheckAvailability(FlightAvailabiltyRequest req) =>
            ListAvailability(req).Any(a => a.AvailableSeats >= req.Pax);


        public IEnumerable<FlightAvailabilityResponse> ListAvailability(FlightAvailabiltyRequest req)
        {
            var result = from eachFlight in _dbCtx.Flights.AsNoTracking()
                         join eachBooking in _dbCtx.Bookings.AsNoTracking() on eachFlight.Id equals eachBooking.FlightId
                         join eachFromCity in _dbCtx.Cities.AsNoTracking() on eachFlight.DepartingCityId equals eachFromCity.Id
                         join eachToCity in _dbCtx.Cities.AsNoTracking() on eachFlight.ArrivalCityId equals eachToCity.Id
                         where eachFromCity.CityName == req.CityFrom &&
                            eachToCity.CityName == req.CityTo &&
                            eachBooking.TravelDay >= req.StartDate &&
                            eachBooking.TravelDay <= req.EndDate
                         select new { eachFlight, eachBooking } into joined
                         group joined by new { joined.eachFlight.FlightName, joined.eachBooking.TravelDay } into grouped
                         select new FlightAvailabilityResponse()
                         {
                             FlightName = grouped.FirstOrDefault().eachFlight.FlightName,
                             TravelDay = grouped.FirstOrDefault().eachBooking.TravelDay,
                             AvailableSeats = grouped.FirstOrDefault().eachFlight.PassengerCapacity.Value - grouped.Count()
                         };

            return result;

        }

        public IEnumerable<FlightDTO> List() => _dbCtx.Flights.AsNoTracking().Select(new FlightMapper().SelectorExpression);


    }
}

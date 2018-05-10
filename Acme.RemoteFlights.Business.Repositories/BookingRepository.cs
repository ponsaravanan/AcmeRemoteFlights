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
    public class BookingRepository : IBookingRepository
    {
        FlightDbContext _dbCtx;
        private bool IsValid(BookingRequest req)
        {
            if (!_dbCtx.Flights.Any(flight => flight.FlightNo == req.FlightNumber)) throw new Exception("Flight number not found");            
            if (req.TravelDay < DateTime.Now) throw new Exception("Can't book earlier than today");

            return true;
        }

        public BookingRepository(FlightDbContext dbContext)
        {
            _dbCtx = dbContext;
        }
        public bool MakeBooking(BookingRequest req)
        {
            if (!IsValid(req)) return false;
            var foundPassengers = _dbCtx.Passengers.Where(person => person.EmailAddress == req.EmailAddress);            
            CreateBooking(req, foundPassengers.Any() ? foundPassengers.First() : CreatePassenger(req));
            return true;
        }

        private void CreateBooking(BookingRequest req, Passenger foundPassenger)
        {
            var booking = new Booking();
            booking.FlightId = _dbCtx.Flights.FirstOrDefault(flight => flight.FlightNo == req.FlightNumber)?.Id;
            booking.PassengerId = foundPassenger.Id;
            booking.TravelDay = req.TravelDay;
            if (_dbCtx.Bookings.Any(xb => xb.FlightId == booking.FlightId &&
                                         xb.PassengerId == foundPassenger.Id &&
                                         xb.TravelDay.Value.Date == req.TravelDay.Date)) throw new Exception("Booking already exists");

            _dbCtx.Bookings.Add(booking);
            _dbCtx.SaveChanges();
        }

        private Passenger CreatePassenger(BookingRequest req)
        {
            Passenger foundPassenger = new Passenger();
            foundPassenger.FirstName = req.FirstName;
            foundPassenger.LastName = req.LastName;
            foundPassenger.EmailAddress = req.EmailAddress;
            
            _dbCtx.Passengers.Add(foundPassenger);
            _dbCtx.SaveChanges();
            return foundPassenger;
        }

        public IEnumerable<BookingSearchResponse> Search(BookingSearchRequest req)
        {
            var result = from eachFlight in _dbCtx.Flights.AsNoTracking()
                         join eachBooking in _dbCtx.Bookings.AsNoTracking() on eachFlight.Id equals eachBooking.FlightId
                         join eachFromCity in _dbCtx.Cities.AsNoTracking() on eachFlight.DepartingCityId equals eachFromCity.Id
                         join eachToCity in _dbCtx.Cities.AsNoTracking() on eachFlight.ArrivalCityId equals eachToCity.Id
                         join eachPassenger in _dbCtx.Passengers.AsNoTracking() on eachBooking.PassengerId equals eachPassenger.Id
                         where (string.IsNullOrEmpty(req.DepartureCity) || eachFromCity.CityName == req.DepartureCity) &&
                            (string.IsNullOrEmpty(req.ArrivalCity) || eachToCity.CityName == req.ArrivalCity) &&
                            (!req.TravelDate.HasValue || eachBooking.TravelDay == req.TravelDate) &&
                            (string.IsNullOrEmpty(req.FlightNumber) || eachFlight.FlightNo == req.FlightNumber) &&
                            (string.IsNullOrEmpty(req.PassengerName) || req.PassengerName == eachPassenger.FirstName + " " + eachPassenger.LastName)
                         select new { eachFlight, eachBooking, eachPassenger };

            var formed = result.AsEnumerable().Select(joined => new BookingSearchResponse()
            {
                ArrivalTime = joined.eachBooking.TravelDay.Value.AddTicks(joined.eachFlight.FlightArrivalTime.Ticks),
                DepartureTime = joined.eachBooking.TravelDay.Value.AddTicks(joined.eachFlight.FlightBoardingTime.Ticks),
                FlightName = joined.eachFlight.FlightName,
                FlightNo = joined.eachFlight.FlightNo,
                PassengerFirstName = joined.eachPassenger.FirstName,
                PassengerLastName = joined.eachPassenger.LastName
            });

            var y = formed.ToList();

            return formed;
        }
    }
}

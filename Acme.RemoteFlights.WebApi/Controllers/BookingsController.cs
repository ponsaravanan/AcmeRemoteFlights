using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acme.RemoteFlights.Business.Contracts;
using Acme.RemoteFlights.Data;
using Acme.RemoteFlights.Dto.Models;
using Acme.RemoteFlights.Dto.Models.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Acme.RemoteBookings.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        private IBookingRepository _BookingRep;
        public BookingsController(IBookingRepository BookingRep)
        {
            _BookingRep = BookingRep;
        }
        // GET api/values
        [HttpPost("search-bookings")]
        public IEnumerable<BookingSearchResponse> Search([FromBody] BookingSearchRequest req) => _BookingRep.Search(req);

        [HttpPost("make-booking")]
        public bool MakeBooking([FromBody] BookingRequest bookingData) => _BookingRep.MakeBooking(bookingData);


    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.RemoteFlights.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.RemoteFlights.Business.Contracts;
using Acme.RemoteFlights.Common;
using Acme.RemoteFlights.Data;
using Acme.RemoteFlights.Dto.Models;

namespace Acme.RemoteFlights.Business.Repositories.Tests
{
    [TestClass()]
    public class BookingRepositoryTests
    {
        private IBookingRepository _bookingRep;
        private FlightDbContext _dbContext;
        private int _lastBookingId;
        private int _lastPassengerId;
        [TestInitialize]
        public void Init()
        {
            _dbContext = new FlightDbContext("FlightDbContext");
            _bookingRep = new BookingRepository(_dbContext, new FlightRepository(_dbContext));
            _lastBookingId = _dbContext.Bookings.Max(b => b.Id);
            _lastPassengerId = _dbContext.Passengers.Max(p => p.Id);
        }

        [TestMethod()]
        public void MakeBookingTest()
        {
            try
            {
                var bookRef = _bookingRep.MakeBooking(new BookingRequest()
                {
                    EmailAddress = "pon.testdata@test.com",
                    FirstName = "Pon test name",
                    LastName = "last test name",
                    FlightNumber = "MS101",
                    TravelDay = new DateTime(2018, 5, 16)
                });
                Assert.IsTrue(bookRef);
            }
            catch (HttpException e)
            {
                Console.WriteLine(e);
                Assert.IsTrue(false);
            }
        }
        [TestMethod()]
        public void MakeBookingShouldFailOnInvalidDate()
        {
            try
            {
                var bookRef = _bookingRep.MakeBooking(new BookingRequest()
                {
                    EmailAddress = "pon.testdata5@test.com",
                    FirstName = "Pon test name5",
                    LastName = "last test name5",
                    FlightNumber = "MS101",
                    TravelDay = new DateTime(2018, 5, 1)
                });
                Assert.IsFalse(bookRef);
            }
            catch (HttpException e)
            {
                Assert.AreEqual(e.Message, "Can't book earlier than today");
                
            }
        }
        [TestMethod()]
        public void MakeBookingShouldFailOnNoAvailableSeat()
        {
            try
            {
                var bookRef = _bookingRep.MakeBooking(new BookingRequest()
                {
                    EmailAddress = "pon.testdata0@test.com",
                    FirstName = "Pon test name1",
                    LastName = "last test name1",
                    FlightNumber = "MS101",
                    TravelDay = new DateTime(2018, 5, 16)
                });
                bookRef = _bookingRep.MakeBooking(new BookingRequest()
                {
                    EmailAddress = "pon.testdata1@test.com",
                    FirstName = "Pon test name1",
                    LastName = "last test name1",
                    FlightNumber = "MS101",
                    TravelDay = new DateTime(2018, 5, 16)
                });
                Assert.IsFalse(bookRef);
            }
            catch (HttpException e)
            {
                Assert.AreEqual(e.Message, "No seats are available");
                Console.WriteLine(e);
                
            }
        }
        [TestMethod()]
        public void MakeBookingShouldFailOnDuplicateBooking()
        {
            try
            {
                var bookRef = _bookingRep.MakeBooking(new BookingRequest()
                {
                    EmailAddress = "pon.testdata3@test.com",
                    FirstName = "Pon test name3",
                    LastName = "last test name3",
                    FlightNumber = "MS101",
                    TravelDay = new DateTime(2018, 5, 17)
                });
                bookRef = _bookingRep.MakeBooking(new BookingRequest()
                {
                    EmailAddress = "pon.testdata3@test.com",
                    FirstName = "Pon test name3",
                    LastName = "last test name3",
                    FlightNumber = "MS101",
                    TravelDay = new DateTime(2018, 5, 17)
                });
                Assert.IsFalse(bookRef);
            }
            catch (HttpException e)
            {
                Assert.AreEqual(e.Message, "Booking already exists");
                Console.WriteLine(e);

            }
        }
        [TestCleanup]
        public void CleanAfterTest()
        {
            _dbContext.Passengers.RemoveRange(_dbContext.Passengers.Where(p => p.Id > _lastPassengerId));
            _dbContext.SaveChanges();
            
            _dbContext.Bookings.RemoveRange(_dbContext.Bookings.Where(b => b.Id > _lastBookingId));
            _dbContext.SaveChanges();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acme.RemoteFlights.Dto.Models.Infrastructure;
using Acme.RemoteFlights.Data;

namespace Acme.RemoteFlights.Dto.Models
{
    public class BookingDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int Id { get; set; }
        public int? PassengerId { get; set; }
        public int? FlightId { get; set; }
        public DateTime? TravelDay { get; set; }
    }

    public class BookingMapper : MapperBase<Booking, BookingDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Booking, BookingDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Booking, BookingDTO>>)(p => new BookingDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    Id = p.Id,
                    PassengerId = p.PassengerId,
                    FlightId = p.FlightId,
                    TravelDay = p.TravelDay
                }));
            }
        }

        public override void MapToModel(BookingDTO dto, Booking model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.Id = dto.Id;
            model.PassengerId = dto.PassengerId;
            model.FlightId = dto.FlightId;
            model.TravelDay = dto.TravelDay;

        }
    }
}

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
    public class FlightDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int Id { get; set; }
        public string FlightName { get; set; }
        public string FlightNo { get; set; }
        public TimeSpan FlightBoardingTime { get; set; }
        public TimeSpan FlightArrivalTime { get; set; }
        public int? PassengerCapacity { get; set; }
        public int DepartingCityId { get; set; }
        public int ArrivalCityId { get; set; }
    }

    public class FlightMapper : MapperBase<Flight, FlightDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<Flight, FlightDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<Flight, FlightDTO>>)(p => new FlightDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    Id = p.Id,
                    FlightName = p.FlightName,
                    FlightNo = p.FlightNo,
                    FlightBoardingTime = p.FlightBoardingTime,
                    FlightArrivalTime = p.FlightArrivalTime,
                    PassengerCapacity = p.PassengerCapacity,
                    DepartingCityId = p.DepartingCityId,
                    ArrivalCityId = p.ArrivalCityId
                }));
            }
        }

        public override void MapToModel(FlightDTO dto, Flight model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.Id = dto.Id;
            model.FlightName = dto.FlightName;
            model.FlightNo = dto.FlightNo;
            model.FlightBoardingTime = dto.FlightBoardingTime;
            model.FlightArrivalTime = dto.FlightArrivalTime;
            model.PassengerCapacity = dto.PassengerCapacity;
            model.DepartingCityId = dto.DepartingCityId;
            model.ArrivalCityId = dto.ArrivalCityId;

        }
    }
}

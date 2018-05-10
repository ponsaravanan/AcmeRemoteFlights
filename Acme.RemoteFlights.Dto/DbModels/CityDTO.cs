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
    public class CityDTO
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public int Id { get; set; }
        public string CityName { get; set; }
    }

    public class CityMapper : MapperBase<City, CityDTO>
    {
        ////BCC/ BEGIN CUSTOM CODE SECTION 
        ////ECC/ END CUSTOM CODE SECTION 
        public override Expression<Func<City, CityDTO>> SelectorExpression
        {
            get
            {
                return ((Expression<Func<City, CityDTO>>)(p => new CityDTO()
                {
                    ////BCC/ BEGIN CUSTOM CODE SECTION 
                    ////ECC/ END CUSTOM CODE SECTION 
                    Id = p.Id,
                    CityName = p.CityName
                }));
            }
        }

        public override void MapToModel(CityDTO dto, City model)
        {
            ////BCC/ BEGIN CUSTOM CODE SECTION 
            ////ECC/ END CUSTOM CODE SECTION 
            model.Id = dto.Id;
            model.CityName = dto.CityName;

        }
    }
}

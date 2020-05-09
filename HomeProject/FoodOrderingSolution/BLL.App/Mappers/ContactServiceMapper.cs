using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class ContactServiceMapper : BaseMapper<DALAppDTO.Contact, BLLAppDTO.Contact>, IContactServiceMapper
    {
        public ContactServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.ContactType, BLLAppDTO.ContactType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
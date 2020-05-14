using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class ContactMapper : BaseMapper<BLL.App.DTO.Contact, Contact>
    {
        public ContactMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.ContactView, ContactView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public ContactView MapContactView(BLL.App.DTO.ContactView inObject)
        {
            return Mapper.Map<ContactView>(inObject);
        }
        
    }
}
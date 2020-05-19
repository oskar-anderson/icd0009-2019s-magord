using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class BillServiceMapper : BaseMapper<DALAppDTO.Bill, BLLAppDTO.Bill>, IBillServiceMapper
    {
        public BillServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.BillView, BLLAppDTO.BillView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.BillView MapBillView(DALAppDTO.BillView inObject)
        {
            return Mapper.Map<BLLAppDTO.BillView>(inObject);
        }
        
    }
}
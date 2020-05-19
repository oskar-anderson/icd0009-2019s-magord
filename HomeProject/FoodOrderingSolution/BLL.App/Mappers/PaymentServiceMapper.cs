using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PaymentServiceMapper : BaseMapper<DALAppDTO.Payment, BLLAppDTO.Payment>, IPaymentServiceMapper
    {
        public PaymentServiceMapper(): base()
        {
            
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PaymentView, BLLAppDTO.PaymentView>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Bill, BLLAppDTO.Bill>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.PaymentType, BLLAppDTO.PaymentType>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
        public BLLAppDTO.PaymentView MapPaymentView(DALAppDTO.PaymentView inObject)
        {
            return Mapper.Map<BLLAppDTO.PaymentView>(inObject);
        }
        
    }
}
using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class PaymentMapper : BaseMapper<BLL.App.DTO.Payment, Payment>
    {
        public PaymentMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.PaymentView, PaymentView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public PaymentView MapPaymentView(BLL.App.DTO.PaymentView inObject)
        {
            return Mapper.Map<PaymentView>(inObject);
        }
        
    }
}
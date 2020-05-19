using AutoMapper;

namespace PublicApi.DTO.v1.Mappers
{
    public class BillMapper : BaseMapper<BLL.App.DTO.Bill, Bill>
    {
        
        public BillMapper()
        {
            MapperConfigurationExpression.CreateMap<BLL.App.DTO.BillView, BillView>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }

        public BillView MapBillView(BLL.App.DTO.BillView inObject)
        {
            return Mapper.Map<BillView>(inObject);
        }
    }
}
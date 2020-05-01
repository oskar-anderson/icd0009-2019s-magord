using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class AreaServiceMapper : BaseMapper<DALAppDTO.Area, BLLAppDTO.Area>, IAreaServiceMapper
    {
        public AreaServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Town, BLLAppDTO.Town>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
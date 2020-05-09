using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class PriceServiceMapper : BaseMapper<DALAppDTO.Price, BLLAppDTO.Price>, IPriceServiceMapper
    {
        public PriceServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Campaign, BLLAppDTO.Campaign>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Ingredient, BLLAppDTO.Ingredient>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Order, BLLAppDTO.Order>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Drink, BLLAppDTO.Drink>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Food, BLLAppDTO.Food>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
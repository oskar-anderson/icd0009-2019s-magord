using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class OrderServiceMapper : BaseMapper<DALAppDTO.Order, BLLAppDTO.Order>, IOrderServiceMapper
    {
        public OrderServiceMapper() : base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Food, BLLAppDTO.Food>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Ingredient, BLLAppDTO.Ingredient>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Drink, BLLAppDTO.Drink>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Restaurant, BLLAppDTO.Restaurant>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.OrderType, BLLAppDTO.OrderType>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Person, BLLAppDTO.Person>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class FoodServiceMapper : BaseMapper<DALAppDTO.Food, BLLAppDTO.Food>, IFoodServiceMapper
    {
        public FoodServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.FoodType, BLLAppDTO.FoodType>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
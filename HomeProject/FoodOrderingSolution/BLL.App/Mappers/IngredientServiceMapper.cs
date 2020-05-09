using AutoMapper;
using BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class IngredientServiceMapper : BaseMapper<DALAppDTO.Ingredient, BLLAppDTO.Ingredient>, IIngredientServiceMapper
    {
        public IngredientServiceMapper(): base()
        {
            MapperConfigurationExpression.CreateMap<DALAppDTO.Identity.AppUser, BLLAppDTO.Identity.AppUser>();
            MapperConfigurationExpression.CreateMap<DALAppDTO.Food, BLLAppDTO.Food>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
        
    }
}
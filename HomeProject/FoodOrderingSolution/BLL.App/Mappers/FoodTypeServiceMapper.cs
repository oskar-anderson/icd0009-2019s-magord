using ee.itcollege.magord.healthyfood.BLL.Base.Mappers;
using Contracts.BLL.App.Mappers;
using BLLAppDTO=BLL.App.DTO;
using DALAppDTO=DAL.App.DTO;

namespace BLL.App.Mappers
{
    public class FoodTypeServiceMapper : BaseMapper<DALAppDTO.FoodType, BLLAppDTO.FoodType>, IFoodTypeServiceMapper
    {
        
    }
}
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class FoodService : BaseEntityService<IAppUnitOfWork, IFoodRepository, IFoodServiceMapper, DAL.App.DTO.Food,
        BLL.App.DTO.Food>, IFoodService
    {
        public FoodService(IAppUnitOfWork uow) : base(uow, uow.Foods, new FoodServiceMapper())
        {
        }
    }
}
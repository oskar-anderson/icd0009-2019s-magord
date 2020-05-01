using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class DrinkService : BaseEntityService<IAppUnitOfWork, IDrinkRepository, IDrinkServiceMapper, DAL.App.DTO.Drink,
        BLL.App.DTO.Drink>, IDrinkService
    {
        public DrinkService(IAppUnitOfWork uow) : base(uow, uow.Drinks, new DrinkServiceMapper())
        {
        }
    }
}
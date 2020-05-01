using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class IngredientService : BaseEntityService<IAppUnitOfWork, IIngredientRepository, IIngredientServiceMapper, DAL.App.DTO.Ingredient,
        BLL.App.DTO.Ingredient>, IIngredientService
    {
        public IngredientService(IAppUnitOfWork uow) : base(uow, uow.Ingredients, new IngredientServiceMapper())
        {
        }
    }
}
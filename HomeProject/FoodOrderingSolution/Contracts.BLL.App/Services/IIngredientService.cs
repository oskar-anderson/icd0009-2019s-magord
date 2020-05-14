using System;
using BLL.App.DTO;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IIngredientService : IBaseEntityService<Ingredient>, IIngredientRepositoryCustom<IngredientView>
    {
        // TODO: add custom methods
    }
}
using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IFoodTypeService : IFoodTypeRepository<Guid, FoodType>
    {
        // TODO: add custom methods
    }
}
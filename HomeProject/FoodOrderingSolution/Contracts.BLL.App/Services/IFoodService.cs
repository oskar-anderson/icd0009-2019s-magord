using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IFoodService : IFoodRepository<Guid, Food>
    {
        // TODO: add custom methods
    }
}
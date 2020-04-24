using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IRestaurantService : IRestaurantRepository<Guid, Restaurant>
    {
        // TODO: add custom methods
    }
}
using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IDrinkService : IDrinkRepository<Guid, Drink>
    {
        // TODO: add custom methods
    }
}
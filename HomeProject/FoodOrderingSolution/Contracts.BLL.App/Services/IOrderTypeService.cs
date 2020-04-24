using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IOrderTypeService : IOrderTypeRepository<Guid, OrderType>
    {
        // TODO: add custom methods
    }
}
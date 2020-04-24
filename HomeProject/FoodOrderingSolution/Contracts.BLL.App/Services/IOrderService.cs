using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IOrderService : IOrderRepository<Guid, Order>
    {
        // TODO: add custom methods
    }
}
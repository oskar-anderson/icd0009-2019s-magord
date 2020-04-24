using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IPaymentService : IPaymentRepository<Guid, Payment>
    {
        // TODO: add custom methods
    }
}
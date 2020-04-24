using System;
using BLL.App.DTO;
using Contracts.DAL.App.Repositories;


namespace Contracts.BLL.App.Services
{
    public interface IPaymentTypeService : IPaymentTypeRepository<Guid, PaymentType>
    {
        // TODO: add custom methods
    }
}
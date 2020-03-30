﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain;
using PublicApi.DTO.v1.PaymentDTOs;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        Task<IEnumerable<PaymentDTO>> DTOAllAsync();
        Task<PaymentDTO> DTOFirstOrDefaultAsync(Guid id);
    }
}
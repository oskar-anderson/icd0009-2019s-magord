﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IPaymentRepository : IPaymentRepository<Guid, Payment>, IBaseRepository<Payment>
    {
        
    }
    public interface IPaymentRepository<TKey, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new() 
        where TKey : IEquatable<TKey>
    {
        Task<IEnumerable<TDALEntity>> AllAsync();
        Task<TDALEntity> FirstOrDefaultAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task DeleteAsync(Guid id);
        
        
        // DTO methods
        //Task<IEnumerable<PaymentDTO>> DTOAllAsync();
        //Task<PaymentDTO> DTOFirstOrDefaultAsync(Guid id);

    }
}
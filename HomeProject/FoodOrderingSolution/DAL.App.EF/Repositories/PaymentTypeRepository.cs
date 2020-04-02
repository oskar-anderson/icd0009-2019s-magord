using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.PaymentTypeDTOs;

namespace DAL.App.EF.Repositories
{
    public class PaymentTypeRepository : EFBaseRepository<PaymentType, AppDbContext>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<PaymentType> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var paymentType = await FirstOrDefaultAsync(id);
            base.Remove(paymentType);
        }
        
        
        
        public async Task<IEnumerable<PaymentTypeDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(p => new PaymentTypeDTO()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }

        public async Task<PaymentTypeDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();

            var paymentTypeDTO = await query.Select(p => new PaymentTypeDTO()
            {
                Id = p.Id,
                Name = p.Name
            }).FirstOrDefaultAsync();

            return paymentTypeDTO;
        }
    }
}
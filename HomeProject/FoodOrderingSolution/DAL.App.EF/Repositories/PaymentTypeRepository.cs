using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class PaymentTypeRepository : EFBaseRepository<AppDbContext, Domain.PaymentType, DAL.App.DTO.PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.PaymentType, DAL.App.DTO.PaymentType>())
        {
        }
        
        public new async Task<IEnumerable<DAL.App.DTO.PaymentType>> AllAsync()
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.PaymentType> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Where(p => p.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var paymentType = await FirstOrDefaultAsync(id);
            base.Remove(paymentType);
        }
        
        /*
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
        */
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class PaymentTypeRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PaymentType, DAL.App.DTO.PaymentType>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.PaymentType, DAL.App.DTO.PaymentType>())
        {
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
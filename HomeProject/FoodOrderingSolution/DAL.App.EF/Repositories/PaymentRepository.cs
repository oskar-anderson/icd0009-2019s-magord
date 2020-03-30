using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.PaymentDTOs;

namespace DAL.App.EF.Repositories
{
    public class PaymentRepository : EFBaseRepository<Payment, AppDbContext>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<Payment> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await FirstOrDefaultAsync(id);
            base.Remove(payment);
        }
        
        
        public async Task<IEnumerable<PaymentDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new PaymentDTO()
                {
                    Id = c.Id,
                    Amount = c.Amount,
                    TimeMade = c.TimeMade,
                    PaymentTypeId = c.PaymentTypeId,
                    PersonId = c.PersonId,
                    BillId = c.BillId
                })
                .ToListAsync();
        }

        public async Task<PaymentDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var paymentDTO = await query.Select(c => new PaymentDTO()
            {
                Id = c.Id,
                Amount = c.Amount,
                TimeMade = c.TimeMade,
                PaymentTypeId = c.PaymentTypeId,
                PersonId = c.PersonId,
                BillId = c.BillId
            }).FirstOrDefaultAsync();

            return paymentDTO;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class BillRepository : EFBaseRepository<Bill, AppDbContext>, IBillRepository
    {
        public BillRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<Bill>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return await RepoDbSet.Where(b => b.AppUserId == userId).ToListAsync();
        }

        public async Task<Bill> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(b => b.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(b => b.Id == id);
            }

            return await RepoDbSet.AnyAsync(b => b.Id == id && b.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var bill = await FirstOrDefaultAsync(id, userId);
            base.Remove(bill);
        }
        
        
        public async Task<IEnumerable<BillDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(b => b.AppUserId == userId);
            }
            return await query
                .Select(b => new BillDTO()
                {
                    Id = b.Id,
                    TimeIssued = b.TimeIssued,
                    Number = b.Number,
                    Sum = b.Sum,
                    OrderId = b.OrderId,
                    PersonId = b.PersonId,
                })
                .ToListAsync();
        }
        
        public async Task<BillDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(b => b.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(b => b.AppUserId == userId);
            }

            var billDTO = await query.Select(b => new BillDTO()
            {
                Id = b.Id,
                TimeIssued = b.TimeIssued,
                Number = b.Number,
                Sum = b.Sum,
                OrderId = b.OrderId,
                PersonId = b.PersonId
            }).FirstOrDefaultAsync();

            return billDTO;
        }


    }
}
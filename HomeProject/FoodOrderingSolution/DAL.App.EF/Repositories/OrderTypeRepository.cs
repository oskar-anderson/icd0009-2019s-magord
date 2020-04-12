using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.OrderTypeDTOs;

namespace DAL.App.EF.Repositories
{
    public class OrderTypeRepository : EFBaseRepository<OrderType, AppDbContext>, IOrderTypeRepository
    {
        public OrderTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<IEnumerable<OrderType>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }


        public async Task<OrderType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(o => o.Id == id).AsQueryable();
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
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }

            return await RepoDbSet.AnyAsync(a => a.Id == id && a.AppUserId == userId);
        }

        
        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var orderType = await FirstOrDefaultAsync(id, userId);
            base.Remove(orderType);
        }
        
        
        public async Task<IEnumerable<OrderTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query
                .Select(o => new OrderTypeDTO()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Comment = o.Comment
                })
                .ToListAsync();
        }

        public async Task<OrderTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(o => o.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }


            var orderTypeDTO = await query.Select(o => new OrderTypeDTO()
            {
                Id = o.Id,
                Name = o.Name,
                Comment = o.Comment
            }).FirstOrDefaultAsync();

            return orderTypeDTO;
        }
    }
}
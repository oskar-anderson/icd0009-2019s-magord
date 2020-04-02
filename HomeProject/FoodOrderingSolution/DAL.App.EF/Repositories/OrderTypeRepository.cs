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
        
       

        public async Task<OrderType> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(o => o.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var orderType = await FirstOrDefaultAsync(id);
            base.Remove(orderType);
        }
        
        
        public async Task<IEnumerable<OrderTypeDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(o => new OrderTypeDTO()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Comment = o.Comment
                })
                .ToListAsync();
        }

        public async Task<OrderTypeDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(o => o.Id == id).AsQueryable();

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
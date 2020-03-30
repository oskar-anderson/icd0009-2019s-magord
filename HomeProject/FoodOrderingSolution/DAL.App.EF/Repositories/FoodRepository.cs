using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.FoodDTOs;

namespace DAL.App.EF.Repositories
{
    public class FoodRepository : EFBaseRepository<Food, AppDbContext>, IFoodRepository
    {
        public FoodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
       

        public async Task<Food> FirstOrDefaultAsync(Guid id)
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
            var food = await FirstOrDefaultAsync(id);
            base.Remove(food);
        }
        
        
        public async Task<IEnumerable<FoodDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new FoodDTO()
                {
                    Id = c.Id,
                    Description = c.Description,
                    Size = c.Size,
                    Name = c.Name,
                    Amount = c.Amount,
                    FoodTypeId = c.FoodTypeId
                })
                .ToListAsync();
        }

        public async Task<FoodDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var foodDTO = await query.Select(c => new FoodDTO()
            {
                Id = c.Id,
                Description = c.Description,
                Size = c.Size,
                Name = c.Name,
                Amount = c.Amount,
                FoodTypeId = c.FoodTypeId
            }).FirstOrDefaultAsync();

            return foodDTO;
        }
    }
}
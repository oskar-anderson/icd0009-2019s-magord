using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.FoodDTOs;
using PublicApi.DTO.v1.FoodTypeDTOs;

namespace DAL.App.EF.Repositories
{
    public class FoodRepository : EFBaseRepository<Food, AppDbContext>, IFoodRepository
    {
        public FoodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        
        public new async Task<IEnumerable<Food>> AllAsync()
        {
            var query = RepoDbSet
                .Include(f => f.FoodType)
                .AsQueryable();
            
            return await query.ToListAsync();
        }
        
        public async Task<Food> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(f => f.FoodType)
                .Where(f => f.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var food = await FirstOrDefaultAsync(id);
            base.Remove(food);
        }
        
        public async Task<IEnumerable<FoodDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(f => f.FoodType)
                .AsQueryable();
            
            return await query
                .Select(f => new FoodDTO()
                {
                    Id = f.Id,
                    Description = f.Description,
                    Size = f.Size,
                    Name = f.Name,
                    Amount = f.Amount,
                    FoodTypeId = f.FoodTypeId,
                    FoodType = new FoodTypeDTO()
                    {
                        Id = f.FoodType!.Id,
                        Name = f.FoodType.Name
                    }
                })
                .ToListAsync();
        }

        public async Task<FoodDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(f => f.FoodType)
                .Where(f => f.Id == id).AsQueryable();

            var foodDTO = await query.Select(f => new FoodDTO()
            {
                Id = f.Id,
                Description = f.Description,
                Size = f.Size,
                Name = f.Name,
                Amount = f.Amount,
                FoodTypeId = f.FoodTypeId,
                FoodType = new FoodTypeDTO()
                {
                    Id = f.FoodType!.Id,
                    Name = f.FoodType.Name
                }
            }).FirstOrDefaultAsync();

            return foodDTO;
        }
    }
}
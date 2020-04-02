using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.FoodTypeDTOs;

namespace DAL.App.EF.Repositories
{
    public class FoodTypeRepository : EFBaseRepository<FoodType, AppDbContext>, IFoodTypeRepository
    {
        public FoodTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<FoodType> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(f => f.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var foodType = await FirstOrDefaultAsync(id);
            base.Remove(foodType);
        }
        
        
        public async Task<IEnumerable<FoodTypeDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(f => new FoodTypeDTO()
                {
                    Id = f.Id,
                    Name = f.Name,
                })
                .ToListAsync();
        }

        public async Task<FoodTypeDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(f => f.Id == id).AsQueryable();

            var foodTypeDTO = await query.Select(f => new FoodTypeDTO()
            {
                Id = f.Id,
                Name = f.Name,
            }).FirstOrDefaultAsync();

            return foodTypeDTO;
        }
    }
}
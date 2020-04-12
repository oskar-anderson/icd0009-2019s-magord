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
        
        public async Task<IEnumerable<FoodType>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }
        
        public async Task<FoodType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(f => f.Id == id).AsQueryable();
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
            var foodType = await FirstOrDefaultAsync(id, userId);
            base.Remove(foodType);
        }
        
        public async Task<IEnumerable<FoodTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            
            return await query
                .Select(f => new FoodTypeDTO()
                {
                    Id = f.Id,
                    Name = f.Name,
                })
                .ToListAsync();
        }

        public async Task<FoodTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(f => f.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }


            var foodTypeDTO = await query.Select(f => new FoodTypeDTO()
            {
                Id = f.Id,
                Name = f.Name,
            }).FirstOrDefaultAsync();

            return foodTypeDTO;
        }
    }
}
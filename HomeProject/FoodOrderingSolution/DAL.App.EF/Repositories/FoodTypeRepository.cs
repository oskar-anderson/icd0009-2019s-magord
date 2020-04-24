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
    public class FoodTypeRepository : EFBaseRepository<AppDbContext, Domain.FoodType, DAL.App.DTO.FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.FoodType, DAL.App.DTO.FoodType>())
        {
        }
        
        public async Task<IEnumerable<DAL.App.DTO.FoodType>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }
        
        public async Task<DAL.App.DTO.FoodType> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(f => f.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
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
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var foodType = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(foodType.Id);
        }
        
        /*
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
        */
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class FoodRepository : EFBaseRepository<AppDbContext, Domain.Food, DAL.App.DTO.Food>, IFoodRepository
    {
        public FoodRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Food, DAL.App.DTO.Food>())
        {
        }
        
        
        public new async Task<IEnumerable<DAL.App.DTO.Food>> AllAsync()
        {
            var query = RepoDbSet
                .Include(f => f.FoodType)
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }
        
        public async Task<DAL.App.DTO.Food> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(f => f.FoodType)
                .Where(f => f.Id == id).AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var food = await FirstOrDefaultAsync(id);
            base.Remove(food);
        }
        
        /*
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
        }*/
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class FoodRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Food, DAL.App.DTO.Food>, IFoodRepository
    {
        public FoodRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Food, DAL.App.DTO.Food>())
        {
        }

        public override async Task<IEnumerable<Food>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(f => f.FoodType);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Food> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(f => f.FoodType)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
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
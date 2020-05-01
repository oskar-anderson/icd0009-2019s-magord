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
    public class IngredientRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Ingredient, DAL.App.DTO.Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Ingredient, DAL.App.DTO.Ingredient>())
        {
        }

        public override async Task<IEnumerable<Ingredient>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Food);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Ingredient> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(i => i.Food)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

        /*
        public async Task<IEnumerable<IngredientDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(i => i.Food)
                .AsQueryable();
            
            return await query
                .Select(i => new IngredientDTO()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Amount = i.Amount,
                    FoodId = i.FoodId,
                    Food = new FoodDTO()
                    {
                        Id = i.Food!.Id,
                        Amount = i.Food.Amount,
                        Description = i.Food.Description,
                        FoodTypeId = i.Food.FoodTypeId,
                        Name = i.Food.Name,
                        Size = i.Food.Size
                    }
                })
                .ToListAsync();
        }

        public async Task<IngredientDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(i => i.Food)
                .Where(i => i.Id == id).AsQueryable();

            var ingredientDTO = await query.Select(i => new IngredientDTO()
            {
                Id = i.Id,
                Name = i.Name,
                Amount = i.Amount,
                FoodId = i.FoodId,
                Food = new FoodDTO()
                {
                    Id = i.Food!.Id,
                    Amount = i.Food.Amount,
                    Description = i.Food.Description,
                    FoodTypeId = i.Food.FoodTypeId,
                    Name = i.Food.Name,
                    Size = i.Food.Size
                }
            }).FirstOrDefaultAsync();

            return ingredientDTO;
        }
        */
    }
}
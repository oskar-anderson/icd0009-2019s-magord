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
    public class IngredientRepository : EFBaseRepository<AppDbContext, Domain.Ingredient, DAL.App.DTO.Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Ingredient, DAL.App.DTO.Ingredient>())
        {
        }
        
        
        public new async Task<IEnumerable<DAL.App.DTO.Ingredient>> AllAsync()
        {
            var query = RepoDbSet
                .Include(i => i.Food)
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        
        public async Task<DAL.App.DTO.Ingredient> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(i => i.Food)
                .Where(i => i.Id == id).AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var ingredient = await FirstOrDefaultAsync(id);
            base.Remove(ingredient);
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
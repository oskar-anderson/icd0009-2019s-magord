using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.FoodDTOs;
using PublicApi.DTO.v1.IngredientDTOs;

namespace DAL.App.EF.Repositories
{
    public class IngredientRepository : EFBaseRepository<Ingredient, AppDbContext>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        
        public new async Task<IEnumerable<Ingredient>> AllAsync()
        {
            var query = RepoDbSet
                .Include(i => i.Food)
                .AsQueryable();
            
            return await query.ToListAsync();
        }

        
        public async Task<Ingredient> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(i => i.Food)
                .Where(i => i.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var ingredient = await FirstOrDefaultAsync(id);
            base.Remove(ingredient);
        }
        
        
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
    }
}
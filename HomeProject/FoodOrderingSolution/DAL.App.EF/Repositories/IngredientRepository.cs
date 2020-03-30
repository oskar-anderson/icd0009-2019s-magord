using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.IngredientDTOs;

namespace DAL.App.EF.Repositories
{
    public class IngredientRepository : EFBaseRepository<Ingredient, AppDbContext>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<Ingredient> FirstOrDefaultAsync(Guid id)
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
            var ingredient = await FirstOrDefaultAsync(id);
            base.Remove(ingredient);
        }
        
        
        public async Task<IEnumerable<IngredientDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new IngredientDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Amount = c.Amount,
                    FoodId = c.FoodId
                })
                .ToListAsync();
        }

        public async Task<IngredientDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var ingredientDTO = await query.Select(c => new IngredientDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Amount = c.Amount,
                FoodId = c.FoodId
            }).FirstOrDefaultAsync();

            return ingredientDTO;
        }
    }
}
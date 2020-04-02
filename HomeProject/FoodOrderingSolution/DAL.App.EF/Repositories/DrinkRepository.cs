using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.DrinkDTOs;

namespace DAL.App.EF.Repositories
{
    public class DrinkRepository : EFBaseRepository<Drink, AppDbContext>, IDrinkRepository
    {
        public DrinkRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<Drink> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var drink = await FirstOrDefaultAsync(id);
            base.Remove(drink);
        }
        
        
        public async Task<IEnumerable<DrinkDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(d => new DrinkDTO()
                {
                    Id = d.Id,
                    Size = d.Size,
                    Name = d.Name,
                    Amount = d.Amount
                })
                .ToListAsync();
        }

        public async Task<DrinkDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(d => d.Id == id).AsQueryable();

            var drinkDTO = await query.Select(d => new DrinkDTO()
            {
                Id = d.Id,
                Size = d.Size,
                Name = d.Name,
                Amount = d.Amount
            }).FirstOrDefaultAsync();

            return drinkDTO;
        }
    }
}
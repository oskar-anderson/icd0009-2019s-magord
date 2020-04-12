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
        
        public async Task<IEnumerable<Drink>> AllAsync(Guid? userId = null)
        {
            
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync();
        }

        
        

        public async Task<Drink> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
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
            var drink = await FirstOrDefaultAsync(id, userId);
            base.Remove(drink);
        }

        
        
        public async Task<IEnumerable<DrinkDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }
            
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

        public async Task<DrinkDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(d => d.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }


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
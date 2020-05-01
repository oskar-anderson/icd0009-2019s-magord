using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class DrinkRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Drink, DAL.App.DTO.Drink>, IDrinkRepository
    {
        public DrinkRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Drink, DAL.App.DTO.Drink>())
        {
        }


        /*
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
        */
    }
}
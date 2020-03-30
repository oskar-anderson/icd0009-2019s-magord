using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.PersonInRestaurantDTOs;

namespace DAL.App.EF.Repositories
{
    public class PersonInRestaurantRepository : EFBaseRepository<PersonInRestaurant, AppDbContext>, IPersonInRestaurantRepository
    {
        public PersonInRestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<PersonInRestaurant> FirstOrDefaultAsync(Guid id)
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
            var personInRestaurant = await FirstOrDefaultAsync(id);
            base.Remove(personInRestaurant);
        }
        
        
        public async Task<IEnumerable<PersonInRestaurantDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new PersonInRestaurantDTO()
                {
                    Id = c.Id,
                    From = c.From,
                    To = c.To,
                    Role = c.Role,
                    PersonId = c.PersonId,
                    RestaurantId = c.RestaurantId
                })
                .ToListAsync();
        }

        public async Task<PersonInRestaurantDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var personInRestaurantDTO = await query.Select(c => new PersonInRestaurantDTO()
            {
                Id = c.Id,
                From = c.From,
                To = c.To,
                Role = c.Role,
                PersonId = c.PersonId,
                RestaurantId = c.RestaurantId
            }).FirstOrDefaultAsync();

            return personInRestaurantDTO;
        }
    }
}
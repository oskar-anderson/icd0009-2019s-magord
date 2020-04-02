using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.PersonInRestaurantDTOs;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace DAL.App.EF.Repositories
{
    public class PersonInRestaurantRepository : EFBaseRepository<PersonInRestaurant, AppDbContext>, IPersonInRestaurantRepository
    {
        public PersonInRestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public new async Task<IEnumerable<PersonInRestaurant>> AllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .AsQueryable();
            
            return await query.ToListAsync();
        }

        
        public async Task<PersonInRestaurant> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .Where(p => p.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var personInRestaurant = await FirstOrDefaultAsync(id);
            base.Remove(personInRestaurant);
        }
        
        public async Task<IEnumerable<PersonInRestaurantDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .AsQueryable();
            
            return await query
                .Select(p => new PersonInRestaurantDTO()
                {
                    Id = p.Id,
                    From = p.From,
                    To = p.To,
                    Role = p.Role,
                    PersonId = p.PersonId,
                    RestaurantId = p.RestaurantId,
                    Person = new PersonDTO()
                    {
                        Id = p.Person!.Id,
                        DateOfBirth = p.Person.DateOfBirth,
                        FirstName = p.Person.FirstName,
                        LastName = p.Person.LastName,
                        Sex = p.Person.Sex,
                    },
                    Restaurant = new RestaurantDTO()
                    {
                        Id = p.Restaurant!.Id,
                        Address = p.Restaurant.Address,
                        AreaId = p.Restaurant.AreaId,
                        ClosedFrom = p.Restaurant.ClosedFrom,
                        OpenedFrom = p.Restaurant.OpenedFrom,
                        Name = p.Restaurant.Name,
                    }
                })
                .ToListAsync();
        }

        public async Task<PersonInRestaurantDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .Where(p => p.Id == id).AsQueryable();

            var personInRestaurantDTO = await query.Select(p => new PersonInRestaurantDTO()
            {
                Id = p.Id,
                From = p.From,
                To = p.To,
                Role = p.Role,
                PersonId = p.PersonId,
                RestaurantId = p.RestaurantId,
                Person = new PersonDTO()
                {
                    Id = p.Person!.Id,
                    DateOfBirth = p.Person.DateOfBirth,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Sex = p.Person.Sex,
                },
                Restaurant = new RestaurantDTO()
                {
                    Id = p.Restaurant!.Id,
                    Address = p.Restaurant.Address,
                    AreaId = p.Restaurant.AreaId,
                    ClosedFrom = p.Restaurant.ClosedFrom,
                    OpenedFrom = p.Restaurant.OpenedFrom,
                    Name = p.Restaurant.Name,
                }
            }).FirstOrDefaultAsync();

            return personInRestaurantDTO;
        }
    }
}
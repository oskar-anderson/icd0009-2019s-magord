using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonInRestaurantRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.PersonInRestaurant, DAL.App.DTO.PersonInRestaurant>, IPersonInRestaurantRepository
    {
        public PersonInRestaurantRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.PersonInRestaurant, DAL.App.DTO.PersonInRestaurant>())
        {
        }

        public override async Task<IEnumerable<PersonInRestaurant>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.Person)
                .Include(p => p.Restaurant);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<PersonInRestaurant> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(p => p.Person)
                .Include(p => p.Restaurant)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

        /*
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
        */
    }
}
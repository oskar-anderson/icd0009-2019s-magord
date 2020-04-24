using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : EFBaseRepository<AppDbContext, Domain.Person, DAL.App.DTO.Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Person, DAL.App.DTO.Person>())
        {
        }
        
        public async Task<IEnumerable<DAL.App.DTO.Person>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return (await RepoDbSet.Where(o => o.AppUserId == userId).ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }


        public async Task<DAL.App.DTO.Person> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
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
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var person = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(person.Id);
        }


        /*
        public async Task<IEnumerable<PersonDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query
                .Select(p => new PersonDTO()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Sex = p.Sex,
                    DateOfBirth = p.DateOfBirth
                })
                .ToListAsync();
        }

        public async Task<PersonDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(p => p.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var personDTO = await query.Select(p => new PersonDTO()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Sex = p.Sex,
                DateOfBirth = p.DateOfBirth
            }).FirstOrDefaultAsync();

            return personDTO;
        }
        */
    }
}
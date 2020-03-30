using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : EFBaseRepository<Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<Person> FirstOrDefaultAsync(Guid id)
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
            var person = await FirstOrDefaultAsync(id);
            base.Remove(person);
        }
        
        
        public async Task<IEnumerable<PersonDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new PersonDTO()
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Sex = c.Sex,
                    DateOfBirth = c.DateOfBirth
                })
                .ToListAsync();
        }

        public async Task<PersonDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var personDTO = await query.Select(c => new PersonDTO()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Sex = c.Sex,
                DateOfBirth = c.DateOfBirth
            }).FirstOrDefaultAsync();

            return personDTO;
        }
    }
}
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
    public class ContactRepository : EFBaseRepository<AppDbContext, Domain.Contact, DAL.App.DTO.Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Contact, DAL.App.DTO.Contact>())
        {
        }
        
        public new async Task<IEnumerable<DAL.App.DTO.Contact>> AllAsync()
        {
            var query = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }
        
        public async Task<DAL.App.DTO.Contact> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(a => a.Id == id).AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contact = await FirstOrDefaultAsync(id);
            base.Remove(contact);
        }
        
        /*
        public async Task<IEnumerable<ContactDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .AsQueryable();
            
            return await query
                .Select(c => new ContactDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PersonId = c.PersonId,
                    ContactTypeId = c.ContactTypeId,
                    ContactType = new ContactTypeDTO()
                    {
                        Id = c.ContactType!.Id,
                        Name = c.ContactType.Name
                    },
                    Person = new PersonDTO()
                    {
                        Id = c.Person!.Id,
                        DateOfBirth = c.Person.DateOfBirth,
                        FirstName = c.Person.FirstName,
                        LastName = c.Person.LastName,
                        Sex = c.Person.Sex,
                    }
                })
                .ToListAsync();
        }

        public async Task<ContactDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(c => c.Id == id).AsQueryable();

            var contactDTO = await query.Select(c => new ContactDTO()
            {
                Id = c.Id,
                Name = c.Name,
                PersonId = c.PersonId,
                ContactTypeId = c.ContactTypeId,
                ContactType = new ContactTypeDTO()
                {
                    Id = c.ContactType!.Id,
                    Name = c.ContactType.Name
                },
                Person = new PersonDTO()
                {
                    Id = c.Person!.Id,
                    DateOfBirth = c.Person.DateOfBirth,
                    FirstName = c.Person.FirstName,
                    LastName = c.Person.LastName,
                    Sex = c.Person.Sex,
                }
            }).FirstOrDefaultAsync();

            return contactDTO;
        }
        */
    }
}
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
    public class ContactRepository : EFBaseRepository<Contact, AppDbContext>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        public new async Task<IEnumerable<Contact>> AllAsync()
        {
            var query = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .AsQueryable();
            
            return await query.ToListAsync();
        }
        
        public async Task<Contact> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(a => a.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var contact = await FirstOrDefaultAsync(id);
            base.Remove(contact);
        }
        
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
    }
}
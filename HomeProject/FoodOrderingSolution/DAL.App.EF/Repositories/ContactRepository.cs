using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class ContactRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Contact, DAL.App.DTO.Contact>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Contact, DAL.App.DTO.Contact>())
        {
        }

        public override async Task<IEnumerable<Contact>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.ContactType)
                .Include(c => c.Person);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Contact> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(c => c.ContactType)
                .Include(c => c.Person)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
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
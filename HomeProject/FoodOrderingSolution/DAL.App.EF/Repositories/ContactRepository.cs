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
        
        

        public async Task<Contact> FirstOrDefaultAsync(Guid id)
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
            var contact = await FirstOrDefaultAsync(id);
            base.Remove(contact);
        }
        
        
        public async Task<IEnumerable<ContactDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new ContactDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    PersonId = c.PersonId,
                    ContactTypeId = c.ContactTypeId
                })
                .ToListAsync();
        }

        public async Task<ContactDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var contactDTO = await query.Select(c => new ContactDTO()
            {
                Id = c.Id,
                Name = c.Name,
                PersonId = c.PersonId,
                ContactTypeId = c.ContactTypeId
            }).FirstOrDefaultAsync();

            return contactDTO;
        }
    }
}
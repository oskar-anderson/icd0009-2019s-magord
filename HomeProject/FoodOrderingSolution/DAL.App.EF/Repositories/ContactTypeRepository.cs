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
    public class ContactTypeRepository : EFBaseRepository<ContactType, AppDbContext>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        

        public async Task<ContactType> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var contactType = await FirstOrDefaultAsync(id);
            base.Remove(contactType);
        }
        
        
        public async Task<IEnumerable<ContactTypeDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new ContactTypeDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }

        public async Task<ContactTypeDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var contactTypeDTO = await query.Select(c => new ContactTypeDTO()
            {
                Id = c.Id,
                Name = c.Name,
            }).FirstOrDefaultAsync();

            return contactTypeDTO;
        }
    }
}
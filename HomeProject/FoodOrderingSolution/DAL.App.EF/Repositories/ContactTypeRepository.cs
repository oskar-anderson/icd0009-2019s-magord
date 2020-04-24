using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : EFBaseRepository<AppDbContext, Domain.ContactType, DAL.App.DTO.ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.ContactType, DAL.App.DTO.ContactType>())
        {
        }
        
        public new async Task<IEnumerable<DAL.App.DTO.ContactType>> AllAsync()
        {
            var query = RepoDbSet
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }
        

        public async Task<DAL.App.DTO.ContactType> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contactType = await FirstOrDefaultAsync(id);
            base.Remove(contactType);
        }
        
        /*
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
        */
    }
}
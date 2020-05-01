using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.ContactType, DAL.App.DTO.ContactType>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.ContactType, DAL.App.DTO.ContactType>())
        {
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
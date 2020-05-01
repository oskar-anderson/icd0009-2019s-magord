using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Person, DAL.App.DTO.Person>,
        IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Person, DAL.App.DTO.Person>())
        {
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
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;


namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Town, DAL.App.DTO.Town>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.Town, DAL.App.DTO.Town>())
        {
        }
    }
}

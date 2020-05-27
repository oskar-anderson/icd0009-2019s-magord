using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;


namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Town, DAL.App.DTO.Town>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.Town, DAL.App.DTO.Town>())
        {
        }
    }
}

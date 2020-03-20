using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TownRepository : EFBaseRepository<Town, AppDbContext>, ITownRepository
    {
        public TownRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
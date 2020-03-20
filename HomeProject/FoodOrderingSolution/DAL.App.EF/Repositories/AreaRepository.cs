using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class AreaRepository : EFBaseRepository<Area, AppDbContext>, IAreaRepository
    {
        public AreaRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Area>> AllAsync()
        {
            return await RepoDbSet.Where(a => a.Name.StartsWith("a")).ToListAsync();
        }
    }
}
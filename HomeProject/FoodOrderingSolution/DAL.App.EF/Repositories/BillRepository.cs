using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BillRepository : EFBaseRepository<Bill, AppDbContext>, IBillRepository
    {
        public BillRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
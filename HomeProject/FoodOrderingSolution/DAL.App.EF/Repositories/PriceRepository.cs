using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository : EFBaseRepository<Price, AppDbContext>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
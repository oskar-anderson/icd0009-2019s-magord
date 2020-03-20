using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodRepository : EFBaseRepository<Food, AppDbContext>, IFoodRepository
    {
        public FoodRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
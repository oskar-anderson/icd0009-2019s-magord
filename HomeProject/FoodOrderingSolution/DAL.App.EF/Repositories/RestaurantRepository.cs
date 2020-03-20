using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class RestaurantRepository : EFBaseRepository<Restaurant, AppDbContext>, IRestaurantRepository
    {
        public RestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class FoodTypeRepository : BaseRepository<FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class IngredientRepository : EFBaseRepository<Ingredient, AppDbContext>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
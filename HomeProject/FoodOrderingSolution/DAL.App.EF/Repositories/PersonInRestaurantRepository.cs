using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonInRestaurantRepository : EFBaseRepository<PersonInRestaurant, AppDbContext>, IPersonInRestaurantRepository
    {
        public PersonInRestaurantRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
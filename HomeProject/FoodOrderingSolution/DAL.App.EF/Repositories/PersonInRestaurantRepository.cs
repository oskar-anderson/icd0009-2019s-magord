using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonInRestaurantRepository : BaseRepository<PersonInRestaurant>, IPersonInRestaurantRepository
    {
        public PersonInRestaurantRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
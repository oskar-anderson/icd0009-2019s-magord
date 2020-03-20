using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : EFBaseRepository<Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
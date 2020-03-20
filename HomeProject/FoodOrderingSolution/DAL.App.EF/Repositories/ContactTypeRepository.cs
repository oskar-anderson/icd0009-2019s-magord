using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactTypeRepository : EFBaseRepository<ContactType, AppDbContext>, IContactTypeRepository
    {
        public ContactTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
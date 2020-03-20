using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ContactRepository : EFBaseRepository<Contact, AppDbContext>, IContactRepository
    {
        public ContactRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
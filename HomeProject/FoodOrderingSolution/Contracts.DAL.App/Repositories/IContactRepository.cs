using Contracts.DAL.Base.Repositories;
using Domain;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepository : IBaseRepository<Contact>
    {
        // add  custom methods here!
    }
}
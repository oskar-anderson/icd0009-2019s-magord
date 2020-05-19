using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IContactRepositoryCustom : IContactRepositoryCustom<ContactView>
    {
    }
    
    public interface IContactRepositoryCustom<TContactView>
    {
        Task<IEnumerable<TContactView>> GetAllForViewAsync(object? userId = null, bool noTracking = true);
        Task<TContactView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true);
    }
    
}
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
        Task<IEnumerable<TContactView>> GetAllForViewAsync();
        Task<TContactView> FirstOrDefaultForViewAsync(Guid id);
    }
    
}
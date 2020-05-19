using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IBillRepositoryCustom : IBillRepositoryCustom<BillView>
    {
        
    }
    
    public interface IBillRepositoryCustom<TBillView>
    {
        Task<IEnumerable<TBillView>> GetAllForViewAsync();
        Task<TBillView> FirstOrDefaultForViewAsync(Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepositoryCustom : IPaymentRepositoryCustom<PaymentView>
    {
        
    }
    
    public interface IPaymentRepositoryCustom<TPaymentView>
    {
        Task<IEnumerable<TPaymentView>> GetAllForViewAsync();
        Task<TPaymentView> FirstOrDefaultForViewAsync(Guid id);
    }
}
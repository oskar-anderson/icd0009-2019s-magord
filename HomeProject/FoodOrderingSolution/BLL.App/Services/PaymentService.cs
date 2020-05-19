using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PaymentService : BaseEntityService<IAppUnitOfWork, IPaymentRepository, IPaymentServiceMapper, DAL.App.DTO.Payment,
        BLL.App.DTO.Payment>, IPaymentService
    {
        public PaymentService(IAppUnitOfWork uow) : base(uow, uow.Payments, new PaymentServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<PaymentView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapPaymentView(e));
        }
        
        public virtual async Task<PaymentView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapPaymentView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}
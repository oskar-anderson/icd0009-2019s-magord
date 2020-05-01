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
    }
}
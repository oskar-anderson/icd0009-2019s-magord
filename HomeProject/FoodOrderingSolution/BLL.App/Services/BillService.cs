using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class BillService : BaseEntityService<IAppUnitOfWork, IBillRepository, IBillServiceMapper, DAL.App.DTO.Bill,
        BLL.App.DTO.Bill>, IBillService
    {
        public BillService(IAppUnitOfWork uow) : base(uow, uow.Bills, new BillServiceMapper())
        {
        }
    }
}
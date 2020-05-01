using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PriceService : BaseEntityService<IAppUnitOfWork, IPriceRepository, IPriceServiceMapper, DAL.App.DTO.Price,
        BLL.App.DTO.Price>, IPriceService
    {
        public PriceService(IAppUnitOfWork uow) : base(uow, uow.Prices, new PriceServiceMapper())
        {
        }
    }
}
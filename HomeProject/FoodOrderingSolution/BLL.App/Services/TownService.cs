using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class TownService : BaseEntityService<IAppUnitOfWork, ITownRepository, ITownServiceMapper, DAL.App.DTO.Town,
        BLL.App.DTO.Town>, ITownService
    {
        public TownService(IAppUnitOfWork uow) : base(uow, uow.Towns, new TownServiceMapper())
        {
        }
    }
}
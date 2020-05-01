using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class AreaService : BaseEntityService<IAppUnitOfWork, IAreaRepository, IAreaServiceMapper, DAL.App.DTO.Area,
        BLL.App.DTO.Area>, IAreaService
    {
        public AreaService(IAppUnitOfWork uow) : base(uow, uow.Areas, new AreaServiceMapper())
        {
        }
    }
}
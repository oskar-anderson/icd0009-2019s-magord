using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ContactTypeService : BaseEntityService<IAppUnitOfWork, IContactTypeRepository, IContactTypeServiceMapper, DAL.App.DTO.ContactType,
        BLL.App.DTO.ContactType>, IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork uow) : base(uow, uow.ContactTypes, new ContactTypeServiceMapper())
        {
        }
    }
}
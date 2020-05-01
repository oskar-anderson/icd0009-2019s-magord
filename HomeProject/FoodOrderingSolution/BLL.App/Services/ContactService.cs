using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class ContactService : BaseEntityService<IAppUnitOfWork, IContactRepository, IContactServiceMapper, DAL.App.DTO.Contact,
        BLL.App.DTO.Contact>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(uow, uow.Contacts, new ContactServiceMapper())
        {
        }
    }
}
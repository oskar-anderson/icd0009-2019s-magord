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
    public class ContactService : BaseEntityService<IAppUnitOfWork, IContactRepository, IContactServiceMapper, DAL.App.DTO.Contact,
        BLL.App.DTO.Contact>, IContactService
    {
        public ContactService(IAppUnitOfWork uow) : base(uow, uow.Contacts, new ContactServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<ContactView>> GetAllForViewAsync(object? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapContactView(e));
        }
        
        public virtual async Task<ContactView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            return Mapper.MapContactView(await Repository.FirstOrDefaultForViewAsync(id, userId));
        }
    }
}
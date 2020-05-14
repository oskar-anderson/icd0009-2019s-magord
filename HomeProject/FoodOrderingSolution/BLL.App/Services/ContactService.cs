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
        
        public virtual async Task<IEnumerable<ContactView>> GetAllForViewAsync()
        {
            return (await Repository.GetAllForViewAsync()).Select(e => Mapper.MapContactView(e));
        }
        
        public virtual async Task<ContactView> FirstOrDefaultForViewAsync(Guid id)
        {
            return Mapper.MapContactView(await Repository.FirstOrDefaultForViewAsync(id));
        }
    }
}
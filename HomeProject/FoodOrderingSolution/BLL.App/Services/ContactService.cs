using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class ContactService : BaseEntityService<IContactRepository, IAppUnitOfWork, DAL.App.DTO.Contact, BLL.App.DTO.Contact>,
        IContactService
    {
        public ContactService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Contact, BLL.App.DTO.Contact>(), unitOfWork.Contacts)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Contact>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Contact> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<ContactDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<ContactDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}
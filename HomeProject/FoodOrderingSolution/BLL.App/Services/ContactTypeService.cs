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
    public class ContactTypeService : BaseEntityService<IContactTypeRepository, IAppUnitOfWork, DAL.App.DTO.ContactType, BLL.App.DTO.ContactType>,
        IContactTypeService
    {
        public ContactTypeService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.ContactType, BLL.App.DTO.ContactType>(), unitOfWork.ContactTypes)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.ContactType>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.ContactType> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<ContactTypeDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<ContactTypeDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}
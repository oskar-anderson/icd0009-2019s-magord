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
    public class PaymentTypeService : BaseEntityService<IPaymentTypeRepository, IAppUnitOfWork, DAL.App.DTO.PaymentType, BLL.App.DTO.PaymentType>,
        IPaymentTypeService
    {
        public PaymentTypeService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.PaymentType, BLL.App.DTO.PaymentType>(), unitOfWork.PaymentTypes)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.PaymentType>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.PaymentType> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<PaymentTypeDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<PaymentTypeDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}
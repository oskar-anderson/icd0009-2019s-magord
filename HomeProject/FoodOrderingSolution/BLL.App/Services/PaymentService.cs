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
    public class PaymentService : BaseEntityService<IPaymentRepository, IAppUnitOfWork, DAL.App.DTO.Payment, BLL.App.DTO.Payment>,
        IPaymentService
    {
        public PaymentService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Payment, BLL.App.DTO.Payment>(), unitOfWork.Payments)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Payment>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Payment> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<PaymentDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<PaymentDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}
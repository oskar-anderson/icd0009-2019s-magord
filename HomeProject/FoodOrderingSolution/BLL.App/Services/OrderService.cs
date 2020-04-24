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
    public class OrderService : BaseEntityService<IOrderRepository, IAppUnitOfWork, DAL.App.DTO.Order, BLL.App.DTO.Order>,
        IOrderService
    {
        public OrderService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Order, BLL.App.DTO.Order>(), unitOfWork.Orders)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Order>> AllAsync(Guid? userId = null) =>
            (await ServiceRepository.AllAsync(userId)).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Order> FirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId));
        
        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.ExistsAsync(id, userId);

        
        public async Task DeleteAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DeleteAsync(id, userId);

        /*
        public async Task<IEnumerable<OrderDTO>> DTOAllAsync(Guid? userId = null) =>
            await ServiceRepository.DTOAllAsync(userId);

        public async Task<OrderDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id, userId);
        */
    }
}
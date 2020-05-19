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
    public class OrderService : BaseEntityService<IAppUnitOfWork, IOrderRepository, IOrderServiceMapper, DAL.App.DTO.Order,
        BLL.App.DTO.Order>, IOrderService
    {
        public OrderService(IAppUnitOfWork uow) : base(uow, uow.Orders, new OrderServiceMapper())
        {
        }
        
        public virtual async Task<IEnumerable<OrderView>> GetAllForViewAsync(object? userId = null, bool noTracking = true)
        {
            return (await Repository.GetAllForViewAsync(userId)).Select(e => Mapper.MapOrderView(e));
        }
        
        public virtual async Task<OrderView> FirstOrDefaultForViewAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            return Mapper.MapOrderView(await Repository.FirstOrDefaultForViewAsync(id, userId));
        }
    }
}
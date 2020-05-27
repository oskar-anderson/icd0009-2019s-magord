using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class OrderTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.OrderType, DAL.App.DTO.OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.OrderType, DAL.App.DTO.OrderType>())
        {
        }
    }
}
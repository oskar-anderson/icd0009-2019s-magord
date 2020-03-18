using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderTypeRepository : BaseRepository<OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
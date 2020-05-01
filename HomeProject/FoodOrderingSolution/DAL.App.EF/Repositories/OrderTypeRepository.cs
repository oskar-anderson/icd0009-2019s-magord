using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;

namespace DAL.App.EF.Repositories
{
    public class OrderTypeRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.OrderType, DAL.App.DTO.OrderType>, IOrderTypeRepository
    {
        public OrderTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.OrderType, DAL.App.DTO.OrderType>())
        {
        }

        /*
        public async Task<IEnumerable<OrderTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query
                .Select(o => new OrderTypeDTO()
                {
                    Id = o.Id,
                    Name = o.Name,
                    Comment = o.Comment
                })
                .ToListAsync();
        }

        public async Task<OrderTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(o => o.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }


            var orderTypeDTO = await query.Select(o => new OrderTypeDTO()
            {
                Id = o.Id,
                Name = o.Name,
                Comment = o.Comment
            }).FirstOrDefaultAsync();

            return orderTypeDTO;
        }
        */
    }
}
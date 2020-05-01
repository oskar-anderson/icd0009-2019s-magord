using Contracts.DAL.Base.Mappers;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;


namespace DAL.App.EF.Repositories
{
    public class FoodTypeRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.FoodType, DAL.App.DTO.FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.FoodType, DAL.App.DTO.FoodType>())
        {
        }
        
        
        /*
        public async Task<IEnumerable<FoodTypeDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet.AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            
            return await query
                .Select(f => new FoodTypeDTO()
                {
                    Id = f.Id,
                    Name = f.Name,
                })
                .ToListAsync();
        }

        public async Task<FoodTypeDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(f => f.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }


            var foodTypeDTO = await query.Select(f => new FoodTypeDTO()
            {
                Id = f.Id,
                Name = f.Name,
            }).FirstOrDefaultAsync();

            return foodTypeDTO;
        }
        */
    }
}
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain;


namespace DAL.App.EF.Repositories
{
    public class FoodTypeRepository : EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.FoodType, DAL.App.DTO.FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(AppDbContext dbContext) : base(dbContext,
            new BaseMapper<Domain.App.FoodType, DAL.App.DTO.FoodType>())
        {
        }
    }
}
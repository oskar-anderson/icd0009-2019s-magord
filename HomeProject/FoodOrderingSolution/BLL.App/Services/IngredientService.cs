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
    public class IngredientService : BaseEntityService<IIngredientRepository, IAppUnitOfWork, DAL.App.DTO.Ingredient, BLL.App.DTO.Ingredient>,
        IIngredientService
    {
        public IngredientService(IAppUnitOfWork unitOfWork) 
            : base(unitOfWork, new BaseBLLMapper<DAL.App.DTO.Ingredient, BLL.App.DTO.Ingredient>(), unitOfWork.Ingredients)
        {
        }
        
        public async Task<IEnumerable<BLL.App.DTO.Ingredient>> AllAsync() =>
            (await ServiceRepository.AllAsync()).Select( dalEntity => Mapper.Map(dalEntity) );

        public async Task<BLL.App.DTO.Ingredient> FirstOrDefaultAsync(Guid id) =>
            Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id));
        
        public async Task<bool> ExistsAsync(Guid id) =>
            await ServiceRepository.ExistsAsync(id);

        
        public async Task DeleteAsync(Guid id) =>
            await ServiceRepository.DeleteAsync(id);

        /*
        public async Task<IEnumerable<IngredientDTO>> DTOAllAsync() =>
            await ServiceRepository.DTOAllAsync();

        public async Task<IngredientDTO> DTOFirstOrDefaultAsync(Guid id) =>
            await ServiceRepository.DTOFirstOrDefaultAsync(id);
        */
    }
}
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;

namespace BLL.App.Services
{
    public class PersonInRestaurantService : BaseEntityService<IAppUnitOfWork, IPersonInRestaurantRepository, IPersonInRestaurantServiceMapper, DAL.App.DTO.PersonInRestaurant,
        BLL.App.DTO.PersonInRestaurant>, IPersonInRestaurantService
    {
        public PersonInRestaurantService(IAppUnitOfWork uow) : base(uow, uow.PersonsInRestaurants, new PersonInRestaurantServiceMapper())
        {
        }
    }
}
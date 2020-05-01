using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using BLL.Base.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF;


namespace BLL.App.Services
{
    public class RestaurantService : BaseEntityService<IAppUnitOfWork, IRestaurantRepository, IRestaurantServiceMapper, DAL.App.DTO.Restaurant, BLL.App.DTO.Restaurant>, IRestaurantService
    {
        public RestaurantService(IAppUnitOfWork uow) : base(uow, uow.Restaurants, new RestaurantServiceMapper())
        {
        }
    }
}
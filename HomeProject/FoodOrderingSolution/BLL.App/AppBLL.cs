using System;
using System.Threading.Tasks;
using BLL.App.Services;
using BLL.Base;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.DAL.App;
using DAL.App.EF;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        public AppBLL(IAppUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        
        public IAreaService Areas =>
            GetService<IAreaService>(() => new AreaService(UnitOfWork));
        
        public IBillService Bills =>
            GetService<IBillService>(() => new BillService(UnitOfWork));

        public ICampaignService Campaigns =>
            GetService<ICampaignService>(() => new CampaignService(UnitOfWork));

        public IContactService Contacts =>
            GetService<IContactService>(() => new ContactService(UnitOfWork));

        public IContactTypeService ContactTypes =>
            GetService<IContactTypeService>(() => new ContactTypeService(UnitOfWork));

        public IDrinkService Drinks =>
            GetService<IDrinkService>(() => new DrinkService(UnitOfWork));

        public IFoodService Foods =>
            GetService<IFoodService>(() => new FoodService(UnitOfWork));

        public IFoodTypeService FoodTypes =>
            GetService<IFoodTypeService>(() => new FoodTypeService(UnitOfWork));

        public IIngredientService Ingredients =>
            GetService<IIngredientService>(() => new IngredientService(UnitOfWork));

        public IOrderService Orders =>
            GetService<IOrderService>(() => new OrderService(UnitOfWork));

        public IOrderTypeService OrderTypes =>
            GetService<IOrderTypeService>(() => new OrderTypeService(UnitOfWork));

        public IPaymentService Payments =>
            GetService<IPaymentService>(() => new PaymentService(UnitOfWork));

        public IPaymentTypeService PaymentTypes =>
            GetService<IPaymentTypeService>(() => new PaymentTypeService(UnitOfWork));

        public IPersonInRestaurantService PersonInRestaurants =>
            GetService<IPersonInRestaurantService>(() => new PersonInRestaurantService(UnitOfWork));

        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(UnitOfWork));

        public IPriceService Prices =>
            GetService<IPriceService>(() => new PriceService(UnitOfWork));

        public IRestaurantService Restaurants =>
            GetService<IRestaurantService>(() => new RestaurantService(UnitOfWork));
        public ITownService Towns =>
            GetService<ITownService>(() => new TownService(UnitOfWork));
        
    }
}
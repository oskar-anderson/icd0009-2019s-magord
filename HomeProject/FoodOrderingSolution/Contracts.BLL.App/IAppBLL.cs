using Contracts.BLL.App.Services;
using Contracts.BLL.Base;

namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        public IAreaService Areas { get; }
        public IBillService Bills { get; }
        public ICampaignService Campaigns { get; }
        public IContactService Contacts { get; }
        public IContactTypeService ContactTypes { get; }
        public IDrinkService Drinks { get; }
        public IFoodService Foods { get; }
        public IFoodTypeService FoodTypes { get; }
        public IIngredientService Ingredients { get; }
        public IOrderService Orders { get; }
        public IOrderTypeService OrderTypes { get; }
        public IPaymentService Payments { get; }
        public IPaymentTypeService PaymentTypes { get; }
        public IPersonService Persons { get; }
        public IPersonInRestaurantService PersonInRestaurants { get; }
        public IPriceService Prices { get; }
        public IRestaurantService Restaurants { get; }
        public ITownService Towns { get; }
    }
}
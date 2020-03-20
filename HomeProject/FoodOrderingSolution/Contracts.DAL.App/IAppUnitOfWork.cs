using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IAreaRepository Areas { get; }
        IBillRepository Bills { get; }
        ICampaignRepository Campaigns { get; }
        IContactRepository Contacts { get; }
        IContactTypeRepository ContactTypes { get; }
        IDrinkRepository Drinks { get; }
        IFoodRepository Foods { get; }
        IFoodTypeRepository FoodTypes { get; }
        IIngredientRepository Ingredients { get; }
        IOrderRepository Orders { get; }
        IOrderTypeRepository OrderTypes { get; }
        IPaymentRepository Payments { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IPersonInRestaurantRepository PersonsInRestaurants { get; }
        public IPersonRepository Persons { get; }
        public IPriceRepository Prices { get; }
        public IRestaurantRepository Restaurants { get; }
        public ITownRepository Towns { get; }
    }
}
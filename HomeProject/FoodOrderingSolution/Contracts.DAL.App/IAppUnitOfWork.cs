using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork, IBaseEntityTracker
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
        IOrderItemRepository OrderItems { get; }
        IOrderTypeRepository OrderTypes { get; }
        IPaymentRepository Payments { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IPriceRepository Prices { get; }
        IRestaurantRepository Restaurants { get; }
        ITownRepository Towns { get; }
    }
}
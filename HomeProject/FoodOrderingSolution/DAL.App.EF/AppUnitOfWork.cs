using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IAreaRepository Areas => GetRepository<IAreaRepository>(() => new AreaRepository(UowDbContext));
        public IBillRepository Bills => GetRepository<IBillRepository>(() => new BillRepository(UowDbContext));
        public ICampaignRepository Campaigns => GetRepository<ICampaignRepository>(() => new CampaignRepository(UowDbContext));
        public IContactRepository Contacts => GetRepository<IContactRepository>(() => new ContactRepository(UowDbContext));
        public IContactTypeRepository ContactTypes => GetRepository<IContactTypeRepository>(() => new ContactTypeRepository(UowDbContext));
        public IDrinkRepository Drinks => GetRepository<IDrinkRepository>(() => new DrinkRepository(UowDbContext));
        public IFoodRepository Foods => GetRepository<IFoodRepository>(() => new FoodRepository(UowDbContext));
        public IFoodTypeRepository FoodTypes => GetRepository<IFoodTypeRepository>(() => new FoodTypeRepository(UowDbContext));
        public IIngredientRepository Ingredients => GetRepository<IIngredientRepository>(() => new IngredientRepository(UowDbContext));
        public IOrderRepository Orders => GetRepository<IOrderRepository>(() => new OrderRepository(UowDbContext));
        public IOrderTypeRepository OrderTypes => GetRepository<IOrderTypeRepository>(() => new OrderTypeRepository(UowDbContext));
        public IPaymentRepository Payments => GetRepository<IPaymentRepository>(() => new PaymentRepository(UowDbContext));
        public IPaymentTypeRepository PaymentTypes => GetRepository<IPaymentTypeRepository>(() => new PaymentTypeRepository(UowDbContext));
        public IPersonInRestaurantRepository PersonsInRestaurants => GetRepository<IPersonInRestaurantRepository>(() => new PersonInRestaurantRepository(UowDbContext));
        public IPersonRepository Persons => GetRepository<IPersonRepository>(() => new PersonRepository(UowDbContext));
        public IPriceRepository Prices => GetRepository<IPriceRepository>(() => new PriceRepository(UowDbContext));
        public IRestaurantRepository Restaurants => GetRepository<IRestaurantRepository>(() => new RestaurantRepository(UowDbContext));
        public ITownRepository Towns => GetRepository<ITownRepository>(() => new TownRepository(UowDbContext));
    }
}
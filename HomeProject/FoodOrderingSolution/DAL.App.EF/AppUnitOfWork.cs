using System;
using System.Collections.Generic;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }
        
        public IAreaRepository Areas => GetRepository<IAreaRepository>(() => new AreaRepository(UOWDbContext));
        public IBillRepository Bills => GetRepository<IBillRepository>(() => new BillRepository(UOWDbContext));
        public ICampaignRepository Campaigns => GetRepository<ICampaignRepository>(() => new CampaignRepository(UOWDbContext));
        public IContactRepository Contacts => GetRepository<IContactRepository>(() => new ContactRepository(UOWDbContext));
        public IContactTypeRepository ContactTypes => GetRepository<IContactTypeRepository>(() => new ContactTypeRepository(UOWDbContext));
        public IDrinkRepository Drinks => GetRepository<IDrinkRepository>(() => new DrinkRepository(UOWDbContext));
        public IFoodRepository Foods => GetRepository<IFoodRepository>(() => new FoodRepository(UOWDbContext));
        public IFoodTypeRepository FoodTypes => GetRepository<IFoodTypeRepository>(() => new FoodTypeRepository(UOWDbContext));
        public IIngredientRepository Ingredients => GetRepository<IIngredientRepository>(() => new IngredientRepository(UOWDbContext));
        public IOrderRepository Orders => GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));
        public IOrderTypeRepository OrderTypes => GetRepository<IOrderTypeRepository>(() => new OrderTypeRepository(UOWDbContext));
        public IPaymentRepository Payments => GetRepository<IPaymentRepository>(() => new PaymentRepository(UOWDbContext));
        public IPaymentTypeRepository PaymentTypes => GetRepository<IPaymentTypeRepository>(() => new PaymentTypeRepository(UOWDbContext));
        public IPersonInRestaurantRepository PersonsInRestaurants => GetRepository<IPersonInRestaurantRepository>(() => new PersonInRestaurantRepository(UOWDbContext));
        public IPersonRepository Persons => GetRepository<IPersonRepository>(() => new PersonRepository(UOWDbContext));
        public IPriceRepository Prices => GetRepository<IPriceRepository>(() => new PriceRepository(UOWDbContext));
        public IRestaurantRepository Restaurants => GetRepository<IRestaurantRepository>(() => new RestaurantRepository(UOWDbContext));
        public ITownRepository Towns => GetRepository<ITownRepository>(() => new TownRepository(UOWDbContext));
    }
}
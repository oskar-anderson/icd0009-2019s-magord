using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderItemRepository :
        EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.OrderItem, DAL.App.DTO.OrderItem>,
        IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.OrderItem, DAL.App.DTO.OrderItem>())
        {
        }

        public override async Task<IEnumerable<OrderItem>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.AppUser)
                .Include(o => o.Order)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<OrderItem> FirstOrDefaultAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.AppUser)
                .Include(o => o.Order)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }

        public virtual async Task<IEnumerable<OrderItemView>> GetAllForViewAsync(object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            return await query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Order)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price)
                
                .Select(a => new OrderItemView()
                {
                    Id = a.Id,
                    Food = a.Food!.Name,
                    Ingredient = a.Ingredient!.Name,
                    Drink = a.Drink!.Name,
                    Order = a.Order!.Number,
                    Quantity = a.Quantity,
                    DrinkPrice = a.Drink.Price!.Value,
                    FoodPrice = a.Food.Price!.Value,
                    IngredientPrice = a.Ingredient.Price!.Value
                }).ToListAsync();
        }

        public virtual async Task<OrderItemView> FirstOrDefaultForViewAsync(Guid id, object? userId = null,
            bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            return await query
                .Include(o => o.Food)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Ingredient)
                .ThenInclude(o => o!.Price)
                .Include(o => o.Order)
                .Include(o => o.Drink)
                .ThenInclude(o => o!.Price)
                .Where(r => r.Id == id)
                .Select(a => new OrderItemView()
                {
                    Id = a.Id,
                    Food = a.Food!.Name,
                    Ingredient = a.Ingredient!.Name,
                    Drink = a.Drink!.Name,
                    Order = a.Order!.Number,
                    Quantity = a.Quantity,
                })
                .FirstOrDefaultAsync();
        }
    }
}
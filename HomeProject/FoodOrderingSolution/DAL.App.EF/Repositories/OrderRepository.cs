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
    public class OrderRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Order, DAL.App.DTO.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.Order, DAL.App.DTO.Order>())
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .Include(o => o.Ingredient)
                .Include(o => o.Food)
                .Include(o => o.Drink)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Order> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .Include(o => o.Ingredient)
                .Include(o => o.Food)
                .Include(o => o.Drink)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        /*
        public async Task<IEnumerable<OrderDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .Include(o => o.Ingredient)
                .Include(o => o.Food)
                .Include(o => o.Drink)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }
            return await query
                .Select(o => new OrderDTO()
                {
                    Id = o.Id,
                    OrderStatus = o.OrderStatus,
                    Number = o.Number,
                    TimeCreated = o.TimeCreated,
                    FoodId = o.FoodId,
                    IngredientId = o.IngredientId,
                    DrinkId = o.DrinkId,
                    RestaurantId = o.RestaurantId,
                    OrderTypeId = o.OrderTypeId,
                    PersonId = o.PersonId,
                    Food = new FoodDTO()
                    {
                        Id = o.Food!.Id,
                        Description = o.Food.Description,
                        Amount = o.Food.Amount,
                        FoodTypeId = o.Food.FoodTypeId,
                        Name = o.Food.Name,
                        Size = o.Food.Size
                    },
                    Ingredient = new IngredientDTO()
                    {
                        Id = o.Ingredient!.Id,
                        Amount = o.Ingredient.Amount,
                        FoodId = o.Ingredient.FoodId,
                        Name = o.Ingredient.Name
                    },
                    Drink = new DrinkDTO()
                    {
                        Id = o.Drink!.Id,
                        Amount = o.Drink.Amount,
                        Name = o.Drink.Name,
                        Size = o.Drink.Size
                    },
                    Restaurant = new RestaurantDTO()
                    {
                        Id = o.Restaurant!.Id,
                        Address = o.Restaurant.Address,
                        AreaId = o.Restaurant.AreaId,
                        ClosedFrom = o.Restaurant.ClosedFrom,
                        Name = o.Restaurant.Name,
                        OpenedFrom = o.Restaurant.OpenedFrom
                    },
                    OrderType = new OrderTypeDTO()
                    {
                        Id = o.OrderType!.Id,
                        Comment = o.OrderType.Comment,
                        Name = o.OrderType.Name
                    },
                    Person = new PersonDTO()
                    {
                        Id = o.Person!.Id,
                        DateOfBirth = o.Person.DateOfBirth,
                        FirstName = o.Person.FirstName,
                        LastName = o.Person.LastName,
                        Sex = o.Person.Sex,
                    }
                })
                .ToListAsync();
        }
        
        public async Task<OrderDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .Include(o => o.Ingredient)
                .Include(o => o.Food)
                .Include(o => o.Drink)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Where(b => b.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId);
            }

            var orderDTO = await query.Select(o => new OrderDTO()
            {
                Id = o.Id,
                    OrderStatus = o.OrderStatus,
                    Number = o.Number,
                    TimeCreated = o.TimeCreated,
                    FoodId = o.FoodId,
                    IngredientId = o.IngredientId,
                    DrinkId = o.DrinkId,
                    RestaurantId = o.RestaurantId,
                    OrderTypeId = o.OrderTypeId,
                    PersonId = o.PersonId,
                    Food = new FoodDTO()
                    {
                        Id = o.Food!.Id,
                        Description = o.Food.Description,
                        Amount = o.Food.Amount,
                        FoodTypeId = o.Food.FoodTypeId,
                        Name = o.Food.Name,
                        Size = o.Food.Size
                    },
                    Ingredient = new IngredientDTO()
                    {
                        Id = o.Ingredient!.Id,
                        Amount = o.Ingredient.Amount,
                        FoodId = o.Ingredient.FoodId,
                        Name = o.Ingredient.Name
                    },
                    Drink = new DrinkDTO()
                    {
                        Id = o.Drink!.Id,
                        Amount = o.Drink.Amount,
                        Name = o.Drink.Name,
                        Size = o.Drink.Size
                    },
                    Restaurant = new RestaurantDTO()
                    {
                        Id = o.Restaurant!.Id,
                        Address = o.Restaurant.Address,
                        AreaId = o.Restaurant.AreaId,
                        ClosedFrom = o.Restaurant.ClosedFrom,
                        Name = o.Restaurant.Name,
                        OpenedFrom = o.Restaurant.OpenedFrom
                    },
                    OrderType = new OrderTypeDTO()
                    {
                        Id = o.OrderType!.Id,
                        Comment = o.OrderType.Comment,
                        Name = o.OrderType.Name
                    },
                    Person = new PersonDTO()
                    {
                        Id = o.Person!.Id,
                        DateOfBirth = o.Person.DateOfBirth,
                        FirstName = o.Person.FirstName,
                        LastName = o.Person.LastName,
                        Sex = o.Person.Sex,
                        }
            }).FirstOrDefaultAsync();

            return orderDTO;
        }
        */
    }
}
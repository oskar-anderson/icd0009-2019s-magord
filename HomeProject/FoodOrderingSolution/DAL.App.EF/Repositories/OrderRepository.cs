using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : EFBaseRepository<AppDbContext, Domain.Order, DAL.App.DTO.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Order, DAL.App.DTO.Order>())
        {
        }

        public async Task<IEnumerable<DAL.App.DTO.Order>> AllAsync(Guid? userId = null)
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
            
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return (await query.Where(o => o.AppUserId == userId && o.Drink!.AppUserId == userId &&
                                           o.Person!.AppUserId == userId && o.OrderType!.AppUserId == userId).ToListAsync())
                .Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.Order> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(o => o.Person)
                .Include(o => o.Restaurant)
                .Include(o => o.Ingredient)
                .Include(o => o.Food)
                .Include(o => o.Drink)
                .Include(o => o.AppUser)
                .Include(o => o.OrderType)
                .Where(o => o.Id == id).AsQueryable()
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(o => o.AppUserId == userId && o.Drink!.AppUserId == userId
                                                               && o.Person!.AppUserId == userId && o.OrderType!.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(o => o.Id == id);
            }

            return await RepoDbSet.AnyAsync(b => b.Id == id && b.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var order = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(order.Id);

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
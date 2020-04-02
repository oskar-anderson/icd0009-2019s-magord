using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.DrinkDTOs;
using PublicApi.DTO.v1.FoodDTOs;
using PublicApi.DTO.v1.IngredientDTOs;
using PublicApi.DTO.v1.OrderDTOs;
using PublicApi.DTO.v1.OrderTypeDTOs;
using PublicApi.DTO.v1.RestaurantDTOs;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : EFBaseRepository<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> AllAsync(Guid? userId = null)
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
                return await query.ToListAsync();
            }

            return await query.Where(o => o.AppUserId == userId).ToListAsync();
        }

        public async Task<Order> FirstOrDefaultAsync(Guid id, Guid? userId = null)
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
                query = query.Where(o => o.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
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
            var order = await FirstOrDefaultAsync(id, userId);
            base.Remove(order);
        }
        
        
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
    }
}
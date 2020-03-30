using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.OrderDTOs;

namespace DAL.App.EF.Repositories
{
    public class OrderRepository : EFBaseRepository<Order, AppDbContext>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> AllAsync(Guid? userId = null)
        {
            if (userId == null)
            {
                return await base.AllAsync();
            }

            return await RepoDbSet.Where(b => b.AppUserId == userId).ToListAsync();
        }

        public async Task<Order> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(b => b.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(b => b.Id == id);
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
            var query = RepoDbSet.AsQueryable();
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
                    PersonId = o.PersonId
                })
                .ToListAsync();
        }
        
        public async Task<OrderDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(b => b.Id == id).AsQueryable();
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
                PersonId = o.PersonId
            }).FirstOrDefaultAsync();

            return orderDTO;
        }
    }
}
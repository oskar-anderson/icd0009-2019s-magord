﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository : EFBaseRepository<AppDbContext, Domain.Price, DAL.App.DTO.Price>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Price, DAL.App.DTO.Price>())
        {
        }
        
        
        public new async Task<IEnumerable<DAL.App.DTO.Price>> AllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Campaign)
                .Include(p => p.Ingredient)
                .Include(p => p.Order)
                .Include(p => p.Drink)
                .Include(p => p.Food)
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }
        

        public async Task<DAL.App.DTO.Price> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Campaign)
                .Include(p => p.Ingredient)
                .Include(p => p.Order)
                .Include(p => p.Drink)
                .Include(p => p.Food)
                .Where(p => p.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var price = await FirstOrDefaultAsync(id);
            base.Remove(price);
        }
        
        /*
        public async Task<IEnumerable<PriceDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Campaign)
                .Include(p => p.Ingredient)
                .Include(p => p.Order)
                .Include(p => p.Drink)
                .Include(p => p.Food)
                .AsQueryable();
            
            return await query
                .Select(p => new PriceDTO()
                {
                    Id = p.Id,
                    From = p.From,
                    To = p.To,
                    Value = p.Value,
                    IngredientId = p.IngredientId,
                    DrinkId = p.DrinkId,
                    CampaignId = p.CampaignId,
                    FoodId = p.FoodId,
                    OrderId = p.OrderId,
                    Ingredient = new IngredientDTO()
                    {
                        Id = p.Ingredient!.Id,
                        Amount = p.Ingredient.Amount,
                        FoodId = p.Ingredient.FoodId,
                        Name = p.Ingredient.Name,
                    },
                    Drink = new DrinkDTO()
                    {
                        Id = p.Drink!.Id,
                        Amount = p.Drink.Amount,
                        Name = p.Drink.Name,
                        Size = p.Drink.Size
                    },
                    Campaign = new CampaignDTO()
                    {
                        Id = p.Campaign!.Id,
                        Comment = p.Campaign.Comment,
                        From = p.Campaign.From,
                        To = p.Campaign.To,
                        Name = p.Campaign.Name
                        
                    },
                    Food = new FoodDTO()
                    {
                        Id = p.Food!.Id,
                        Amount = p.Food.Amount,
                        Description = p.Food.Description,
                        FoodTypeId = p.Food.FoodTypeId,
                        Name = p.Food.Name,
                        Size = p.Food.Size
                    },
                    Order = new OrderDTO()
                    {
                        Id = p.Order!.Id,
                        OrderStatus = p.Order.OrderStatus,
                        TimeCreated = p.Order.TimeCreated,
                        Number = p.Order.Number,
                        IngredientId = p.Order.IngredientId,
                        FoodId = p.Order.FoodId,
                        PersonId = p.Order.PersonId,
                        RestaurantId =p.Order.RestaurantId,
                        OrderTypeId = p.Order.OrderTypeId,
                        DrinkId = p.Order.DrinkId
                    }
                })
                .ToListAsync();
        }

        public async Task<PriceDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Campaign)
                .Include(p => p.Ingredient)
                .Include(p => p.Order)
                .Include(p => p.Drink)
                .Include(p => p.Food)   
                .Where(p => p.Id == id).AsQueryable();

            var priceDTO = await query.Select(p => new PriceDTO()
            {
                Id = p.Id,
                    From = p.From,
                    To = p.To,
                    Value = p.Value,
                    IngredientId = p.IngredientId,
                    DrinkId = p.DrinkId,
                    CampaignId = p.CampaignId,
                    FoodId = p.FoodId,
                    OrderId = p.OrderId,
                    Ingredient = new IngredientDTO()
                    {
                        Id = p.Ingredient!.Id,
                        Amount = p.Ingredient.Amount,
                        FoodId = p.Ingredient.FoodId,
                        Name = p.Ingredient.Name,
                    },
                    Drink = new DrinkDTO()
                    {
                        Id = p.Drink!.Id,
                        Amount = p.Drink.Amount,
                        Name = p.Drink.Name,
                        Size = p.Drink.Size
                    },
                    Campaign = new CampaignDTO()
                    {
                        Id = p.Campaign!.Id,
                        Comment = p.Campaign.Comment,
                        From = p.Campaign.From,
                        To = p.Campaign.To,
                        Name = p.Campaign.Name
                        
                    },
                    Food = new FoodDTO()
                    {
                        Id = p.Food!.Id,
                        Amount = p.Food.Amount,
                        Description = p.Food.Description,
                        FoodTypeId = p.Food.FoodTypeId,
                        Name = p.Food.Name,
                        Size = p.Food.Size
                    },
                    Order = new OrderDTO()
                    {
                        Id = p.Order!.Id,
                        OrderStatus = p.Order.OrderStatus,
                        TimeCreated = p.Order.TimeCreated,
                        Number = p.Order.Number,
                        IngredientId = p.Order.IngredientId,
                        FoodId = p.Order.FoodId,
                        PersonId = p.Order.PersonId,
                        RestaurantId =p.Order.RestaurantId,
                        OrderTypeId = p.Order.OrderTypeId,
                        DrinkId = p.Order.DrinkId
                    }
            }).FirstOrDefaultAsync();

            return priceDTO;
        }
        */
    }
}
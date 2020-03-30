using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.PriceDTOs;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository : EFBaseRepository<Price, AppDbContext>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        

        public async Task<Price> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            {
                return await RepoDbSet.AnyAsync(a => a.Id == id);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var price = await FirstOrDefaultAsync(id);
            base.Remove(price);
        }
        
        
        public async Task<IEnumerable<PriceDTO>> DTOAllAsync()
        {
            var query = RepoDbSet.AsQueryable();
            
            return await query
                .Select(c => new PriceDTO()
                {
                    Id = c.Id,
                    From = c.From,
                    To = c.To,
                    Value = c.Value,
                    IngredientId = c.IngredientId,
                    DrinkId = c.DrinkId,
                    CampaignId = c.CampaignId,
                    FoodId = c.FoodId,
                    OrderId = c.OrderId
                })
                .ToListAsync();
        }

        public async Task<PriceDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet.Where(c => c.Id == id).AsQueryable();

            var priceDTO = await query.Select(c => new PriceDTO()
            {
                Id = c.Id,
                From = c.From,
                To = c.To,
                Value = c.Value,
                IngredientId = c.IngredientId,
                DrinkId = c.DrinkId,
                CampaignId = c.CampaignId,
                FoodId = c.FoodId,
                OrderId = c.OrderId
            }).FirstOrDefaultAsync();

            return priceDTO;
        }
    }
}
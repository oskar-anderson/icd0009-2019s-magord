using System;
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    // for display only
    public class Price : PriceEdit
    {
        public ICollection<Ingredient> Ingredients { get; set; } = default!;
        public ICollection<Food> Foods { get; set; } = default!;
        public ICollection<Drink> Drinks { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = default!;
        public ICollection<Campaign> Campaigns { get; set; } = default!;
    }

    // for display only
    public class PriceDetail : PriceEdit
    {
        public Ingredient Ingredient { get; set; } = default!;
        public Food Food { get; set; } = default!;
        public Drink Drink { get; set; } = default!;
        public Order Order { get; set; } = default!;
        public Campaign Campaign { get; set; } = default!;
    }

    // from client to server
    public class PriceEdit: PriceCreate
    {
        public Guid Id { get; set; }
    }
    // from client to server
    public class PriceCreate
    {
        public string From { get; set; } = default!;

        public string To { get; set; } = default!;
        
        public decimal Value { get; set; } = default!;
        
        public Guid IngredientId { get; set; }
        public Guid FoodId { get; set; }
        public Guid DrinkId { get; set; }
        public Guid OrderId { get; set; }
        public Guid CampaignId { get; set; }
    }
}
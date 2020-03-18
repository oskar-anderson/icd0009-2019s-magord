using System;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid> // Before was DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<Town> Towns { get; set; } = default!;
        public DbSet<Area> Areas { get; set; } = default!;
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Price> Prices { get; set; } = default!;
        public DbSet<PersonInRestaurant> PersonInRestaurants { get; set; } = default!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<OrderType> OrderTypes { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Ingredient> Ingredients { get; set; } = default!;
        public DbSet<FoodType> FoodTypes { get; set; } = default!;
        public DbSet<Food> Foods { get; set; } = default!;
        public DbSet<Drink> Drinks { get; set; } = default!;
        public DbSet<ContactType> ContactTypes { get; set; } = default!;
        public DbSet<Contact> Contacts { get; set; } = default!;
        public DbSet<Campaign> Campaigns { get; set; } = default!;
        public DbSet<Bill> Bills { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
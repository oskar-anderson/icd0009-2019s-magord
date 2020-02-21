using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; } = default!;
        public DbSet<Town> Towns { get; set; } = default!;
        public DbSet<Area> Areas { get; set; } = default!;
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
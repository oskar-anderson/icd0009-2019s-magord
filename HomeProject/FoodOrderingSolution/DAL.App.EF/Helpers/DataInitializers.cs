using System;
using System.Linq;
using Domain;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }
        public static bool DeleteDatabase(AppDbContext context)
        {
            return context.Database.EnsureDeleted();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            
            var roleNames = new string[] {"User", "Admin"};
            foreach (var roleName in roleNames)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole();
                    role.Name = roleName;
                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }
            
            var userName = "magord@ttu.ee";
            var passWord = "Kala.maja.2020";

            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser();
                user.Email = userName;
                user.UserName = userName;

                var result = userManager.CreateAsync(user, passWord).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");

                }
            }


            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "User").Result;

        }
        
        public static void SeedData(AppDbContext context)
        {
            // insert predefined towns 
            var towns = new Town[]
            {
                new Town()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Tallinn",
                },
                new Town()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Pärnu",
                    
                },
                new Town()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name =  "Tartu",
                },
            };

            foreach (var town in towns)
            {
                if (!context.Towns.Any(l => l.Id == town.Id))
                {
                    context.Towns.Add(town);
                }
            }

            context.SaveChanges();
            
            var foodTypes = new FoodType[]
            {
                new FoodType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Salty",
                },
                new FoodType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Sweet",
                },
            };

            foreach (var foodType in foodTypes)
            {
                if (!context.FoodTypes.Any(l => l.Id == foodType.Id))
                {
                    context.FoodTypes.Add(foodType);
                }
            }
            
            context.SaveChanges();
        }
    }
}

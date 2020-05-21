using System;
using System.Linq;
using System.Net.Http.Headers;
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
            var firstName = "Marko";
            var lastName = "Gordejev";

            var user = userManager.FindByNameAsync(userName).Result;
            if (user == null)
            {
                user = new AppUser();
                user.Email = userName;
                user.UserName = userName;
                user.FirstName = firstName;
                user.LastName = lastName;

                var result = userManager.CreateAsync(user, passWord).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");

                }
            }


            var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
            roleResult = userManager.AddToRoleAsync(user, "User").Result;

        }
        
        public static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            var user = userManager.FindByNameAsync("magord@ttu.ee").Result;
            
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
            
            // insert predefined areas 
            var areas = new Area[]
            {
                new Area()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Mustamäe",
                    Town = towns[0]
                },
                new Area()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Kesklinn",
                    Town = towns[1]
                    
                },
                new Area()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name =  "Supilinn",
                    Town = towns[2]
                },
            };

            foreach (var area in areas)
            {
                if (!context.Areas.Any(l => l.Id == area.Id))
                {
                    context.Areas.Add(area);
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

            var prices = new Price[]
            {
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Value =  6.90M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Value =  5.90M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Value =  1.50M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Value =  2.50M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Value =  0.70M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Value =  3.50M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Value =  4.00M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    Value =  2.00M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    Value =  3.00M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
                new Price()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    Value =  3.90M,
                    From = DateTime.Now.ToString("dd/MM/yyyy"),
                    To = new DateTime(3000, 01, 01).ToString("dd/MM/yyyy"),
                    AppUserId = user.Id
                },
            };

            foreach (var price in prices)
            {
                if (!context.Prices.Any(l => l.Id == price.Id))
                {
                    context.Prices.Add(price);
                }
            }
            
            context.SaveChanges();
            
            var foods = new Food[]
            {
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Chicken-wrap",
                    Size = "S",
                    Amount = 1,
                    Description = "A tasty wrap with chicken and salad",
                    FoodType = foodTypes[0],
                    Price = prices[1]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Shrimp-salad",
                    Size = "M",
                    Amount = 1,
                    Description = "Green salad with shrimp",
                    FoodType = foodTypes[0],
                    Price = prices[0]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name =  "Caesar salad",
                    Size = "M",
                    Amount = 1,
                    Description = "Green salad with chicken and caesar sauce",
                    FoodType = foodTypes[0],
                    Price = prices[0]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Name =  "Turkey sandwich",
                    Size = "S",
                    Amount = 1,
                    Description = "Sandwich with turkey and veggie bits",
                    FoodType = foodTypes[0],
                    Price = prices[9]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Name =  "Noodle chicken soup",
                    Size = "M",
                    Amount = 1,
                    Description = "Soup with noodles and chicken inside",
                    FoodType = foodTypes[0],
                    Price = prices[0]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Name =  "Tacos",
                    Size = "M",
                    Amount = 1,
                    Description = "Tacos with grilled salmon and veggies",
                    FoodType = foodTypes[0],
                    Price = prices[0]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Name =  "Blueberry muffin",
                    Size = "S",
                    Amount = 1,
                    Description = "Dairy free muffin with blueberries",
                    FoodType = foodTypes[1],
                    Price = prices[3]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    Name =  "Fruit salad",
                    Size = "M",
                    Amount = 1,
                    Description = "Salad containing many different fruits",
                    FoodType = foodTypes[1],
                    Price = prices[6]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    Name =  "Oatmeal cookie",
                    Size = "S",
                    Amount = 1,
                    Description = "Freshly baked oatmeal cookie with raisins",
                    FoodType = foodTypes[1],
                    Price = prices[2]
                },
                
                new Food()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    Name =  "Sugar-free pumpkin pie",
                    Size = "S",
                    Amount = 1,
                    Description = "Pumpkin pie with no added sugar",
                    FoodType = foodTypes[1],
                    Price = prices[6]
                },
            };

            foreach (var food in foods)
            {
                if (!context.Foods.Any(l => l.Id == food.Id))
                {
                    context.Foods.Add(food);
                }
            }

            context.SaveChanges();
            
            var restaurants = new Restaurant[]
            {
                new Restaurant()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Healthy-Me",
                    OpenedFrom = "07:00",
                    ClosedFrom = "21:00",
                    Address = "Sõpruse pst. 17",
                    Area = areas[0],
                },
                
                new Restaurant()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Healthy-Me",
                    OpenedFrom = "07:00",
                    ClosedFrom = "21:00",
                    Address = "Rüütli 43",
                    Area = areas[1],
                },
                
                new Restaurant()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name =  "Healthy-Me",
                    OpenedFrom = "07:00",
                    ClosedFrom = "21:00",
                    Address = "Herne 53",
                    Area = areas[2],
                },
                
            };

            foreach (var restaurant in restaurants)
            {
                if (!context.Restaurants.Any(l => l.Id == restaurant.Id))
                {
                    context.Restaurants.Add(restaurant);
                }
            }

            context.SaveChanges();
            
            var orderTypes = new OrderType[]
            {
                new OrderType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Pick-up order",
                },
                
                new OrderType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Delivery order",
                },
                
            };

            foreach (var orderType in orderTypes)
            {
                if (!context.OrderTypes.Any(l => l.Id == orderType.Id))
                {
                    context.OrderTypes.Add(orderType);
                }
            }

            context.SaveChanges();
            
            var drinks = new Drink[]
            {
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Water",
                    Size = 0.5F,
                    Amount = 1,
                    Price = prices[2]
                },
                
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Orange juice",
                    Size = 0.5F,
                    Amount = 1,
                    Price = prices[3]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Name =  "Apple juice",
                    Size = 0.5F,
                    Amount = 1,
                    Price = prices[3]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Name =  "Smoothie",
                    Size = 0.5F,
                    Amount = 1,
                    Price = prices[5]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Name =  "Home-made lemonade",
                    Size = 0.75F,
                    Amount = 1,
                    Price = prices[6]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Name =  "Ice tea",
                    Size = 0.75F,
                    Amount = 1,
                    Price = prices[8]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Name =  "Green tea",
                    Size = 0.3F,
                    Amount = 1,
                    Price = prices[2]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    Name =  "Cappuccino",
                    Size = 0.3F,
                    Amount = 1,
                    Price = prices[7]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    Name =  "Latte",
                    Size = 0.3F,
                    Amount = 1,
                    Price = prices[7]
                },
                new Drink()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    Name =  "Black coffee",
                    Size = 0.3F,
                    Amount = 1,
                    Price = prices[7]
                },
            };

            foreach (var drink in drinks)
            {
                if (!context.Drinks.Any(l => l.Id == drink.Id))
                {
                    context.Drinks.Add(drink);
                }
            }

            context.SaveChanges();
            
            var ingredients = new Ingredient[]
            {
                // For chicken wrap and caesar salad
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Extra chicken",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[0],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Name =  "Extra shrimp",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[1],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Name =  "Extra croutons",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[2],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    Name =  "Extra onion",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[4],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000013"),
                    Name =  "Extra blueberries",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[6],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000015"),
                    Name =  "Extra Raisins",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[8],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000016"),
                    Name =  "Extra turkey",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[3],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000017"),
                    Name =  "Extra salmon",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[5],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000018"),
                    Name =  "Extra pumpkin",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[9],
                },
                new Ingredient()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000019"),
                    Name =  "Honey lime dressing",
                    Amount = 1,
                    Price = prices[4],
                    Food = foods[7],
                },
            };

            foreach (var ingredient in ingredients)
            {
                if (!context.Ingredients.Any(l => l.Id == ingredient.Id))
                {
                    context.Ingredients.Add(ingredient);
                }
            }

            context.SaveChanges();
            
            
            var contactTypes = new ContactType[]
            {
                new ContactType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "E-mail"
                },
                new ContactType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "Phone"
                },
            };

            foreach (var contactType in contactTypes)
            {
                if (!context.ContactTypes.Any(l => l.Id == contactType.Id))
                {
                    context.ContactTypes.Add(contactType);
                }
            }

            context.SaveChanges();
            
            var orders = new Order[]
            {
                new Order()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Number = 222,
                    TimeCreated = "3333",
                    Restaurant = restaurants[0],
                    OrderType = orderTypes[0],
                    OrderStatus = "Underway",
                    AppUserId = user.Id
                    
                },
            };

            foreach (var order in orders)
            {
                if (!context.Orders.Any(l => l.Id == order.Id))
                {
                    context.Orders.Add(order);
                }
            }

            context.SaveChanges();
            
            var orderItems = new OrderItem[]
            {
                new OrderItem()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Quantity = 1,
                    Drink = drinks[0],
                    Order = orders[0],
                    AppUserId = user.Id
                },
            };

            foreach (var orderItem in orderItems)
            {
                if (!context.OrderItems.Any(l => l.Id == orderItem.Id))
                {
                    context.OrderItems.Add(orderItem);
                }
            }

            context.SaveChanges();
            
            var paymentTypes = new PaymentType[]
            {
                new PaymentType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name = "By card",
                },
                new PaymentType()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name = "By cash",
                },
            };

            foreach (var paymentType in paymentTypes)
            {
                if (!context.PaymentTypes.Any(l => l.Id == paymentType.Id))
                {
                    context.PaymentTypes.Add(paymentType);
                }
            }

            context.SaveChanges();
            
        }
    }
}

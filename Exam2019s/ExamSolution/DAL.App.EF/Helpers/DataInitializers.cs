using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
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
            

            var users = new (string email, string password, string firstName, string lastName)[]
            {
                ("magord@ttu.ee", "Kala.maja.2020", "Marko", "Gordejev"),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.email).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.email,
                        UserName = userInfo.email,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                    };

                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "Admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "User").Result;
            }
        }

        public static void SeedData(AppDbContext context, UserManager<AppUser> userManager)
        {
            var user = userManager.FindByNameAsync("magord@ttu.ee").Result;
            
            var quizzes = new Quiz[]
            {
                new Quiz()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Name =  "Country capitals",
                    TotalPoints = 100.00M,
                    AppUser = user
                },
                new Quiz()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Name =  "Movies",
                    TotalPoints = 60.00M,
                    AppUser = user
                },
            };

            foreach (var quiz in quizzes)
            {
                if (!context.Quizzes.Any(q => q.Id == quiz.Id))
                {
                    context.Quizzes.Add(quiz);
                }
            }

            context.SaveChanges();

            var questions = new Question[]
            {
                // Questions for capital cities
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Number = 1,
                    Description = "Capital city of Estonia",
                    Points = 25,
                    Quiz = quizzes[0]
                },
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Number = 2,
                    Description = "Capital city of Russia",
                    Points = 25,
                    Quiz = quizzes[0]
                },
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Number = 3,
                    Description = "Capital city of Spain",
                    Points = 25,
                    Quiz = quizzes[0]
                },
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Number = 4,
                    Description = "Capital city of Belgium",
                    Points = 25,
                    Quiz = quizzes[0]
                },
                // Questions for movies
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Number = 1,
                    Description = "Who directed Titanic, Avatar and The Terminator?",
                    Points = 20,
                    Quiz = quizzes[1]
                },
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Number = 2,
                    Description = "What is the name of the second James Bond film?",
                    Points = 20,
                    Quiz = quizzes[1]
                },
                new Question()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Number = 3,
                    Description = "What does Tom Hanks compare life to in Forest Gump?",
                    Points = 20,
                    Quiz = quizzes[1]
                },
            };

            foreach (var question in questions)
            {
                if (!context.Questions.Any(q => q.Id == question.Id))
                {
                    context.Questions.Add(question);
                }
            }

            context.SaveChanges();

            var choices = new Choice[]
            {
                // For question - "capital city of Estonia"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Value = "Tallinn",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[0]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000002"),
                    Value = "Berlin",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[0]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000003"),
                    Value = "Washington D.C",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[0]
                },
                // For question - "capital city of Russia"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000004"),
                    Value = "London",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[1]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000005"),
                    Value = "Moscow",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[1]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000006"),
                    Value = "Beijing",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[1]
                },
                // For question - "capital city of Spain"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000007"),
                    Value = "Madrid",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[2]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000008"),
                    Value = "Kiev",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[2]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000009"),
                    Value = "Tokyo",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[2]
                },
                // For question - "capital city of Belgium"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000010"),
                    Value = "Ottawa",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[3]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000011"),
                    Value = "Vienna",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[3]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000012"),
                    Value = "Brussels",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[3]
                },
                // For question - "Who directed Titanic, Avatar and The Terminator?"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000013"),
                    Value = "Christopher Nolan",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[4]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000014"),
                    Value = "Andres Käver",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[4]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000015"),
                    Value = "James Cameron",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[4]
                },
                // For question - "What is the name of the second James Bond film?"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000016"),
                    Value = "From Russia With Love",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[5]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000017"),
                    Value = "Live and Let Die",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[5]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000018"),
                    Value = "Dr. No",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[5]
                },
                // For question - "What does Tom Hanks compare life to in Forest Gump??"
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000019"),
                    Value = "A walk in the park",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[6]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000020"),
                    Value = "A box of chocolates",
                    IsSelected = false,
                    IsAnswer = true,
                    Question = questions[6]
                },
                new Choice()
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000021"),
                    Value = "A dog's life",
                    IsSelected = false,
                    IsAnswer = false,
                    Question = questions[6]
                },
                
            };

            foreach (var choice in choices)
            {
                if (!context.Choices.Any(c => c.Id == choice.Id))
                {
                    context.Choices.Add(choice);
                }
            }
            
            context.SaveChanges();
        }
    }
}
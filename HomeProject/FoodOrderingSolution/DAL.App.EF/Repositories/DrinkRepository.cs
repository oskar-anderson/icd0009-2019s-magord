﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DrinkRepository : EFBaseRepository<Drink, AppDbContext>, IDrinkRepository
    {
        public DrinkRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
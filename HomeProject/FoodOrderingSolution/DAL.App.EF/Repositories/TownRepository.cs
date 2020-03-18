﻿using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class TownRepository : BaseRepository<Town>, ITownRepository
    {
        public TownRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
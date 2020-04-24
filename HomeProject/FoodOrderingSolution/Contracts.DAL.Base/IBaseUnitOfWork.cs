﻿using System;
using System.Threading.Tasks;

namespace Contracts.DAL.Base
{
    public interface IBaseUnitOfWork
    {
        // get thing done! COMMIT
        int SaveChanges();
        Task<int> SaveChangesAsync();
        TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod);
    }
}
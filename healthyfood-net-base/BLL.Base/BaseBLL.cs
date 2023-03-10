using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ee.itcollege.magord.healthyfood.Contracts.BLL.Base;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base;

namespace ee.itcollege.magord.healthyfood.BLL.Base
{
    public abstract class BaseBLL<TUnitOfWork> : IBaseBLL
        where TUnitOfWork : IBaseUnitOfWork
    {
        protected readonly TUnitOfWork UnitOfWork;

        private readonly Dictionary<Type, object> _serviceCache = new Dictionary<Type, object>();
        
        protected BaseBLL(TUnitOfWork uow)
        {
            UnitOfWork = uow;
        }

        public Task<int> SaveChangesAsync()
        {
            return UnitOfWork.SaveChangesAsync();
        }

        public TService GetService<TService>(Func<TService> serviceCreationMethod)
            where TService : class
        {
            if (_serviceCache.TryGetValue(typeof(TService), out var repo))
            {
                return (TService) repo;
            }

            var newRepoInstance = serviceCreationMethod();
            _serviceCache.Add(typeof(TService), newRepoInstance);
            return newRepoInstance;
        }
    }
}
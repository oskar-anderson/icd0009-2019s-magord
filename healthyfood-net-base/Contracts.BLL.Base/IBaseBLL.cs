using System;
using System.Threading.Tasks;

namespace ee.itcollege.magord.healthyfood.Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();
        TService GetService<TService>(Func<TService> serviceCreationMethod)
            where TService : class;
    }
}
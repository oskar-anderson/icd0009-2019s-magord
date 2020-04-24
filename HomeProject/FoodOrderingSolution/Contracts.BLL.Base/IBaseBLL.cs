using System;
using System.Threading.Tasks;

namespace Contracts.BLL.Base
{
    public interface IBaseBLL
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();
        TService GetService<TService>(Func<TService> serviceCreationMethod);
    }
}
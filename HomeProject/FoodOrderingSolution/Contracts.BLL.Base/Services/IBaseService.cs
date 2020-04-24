using System;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;

namespace Contracts.BLL.Base.Services
{
    public interface IBaseService
    {
        // add common base methods here
    }
    
    public interface IBaseEntityService<TBLLEntity> : IBaseService, IBaseRepository<Guid, TBLLEntity> 
        where TBLLEntity : class, IDomainBaseEntity<Guid>, new()

    {
    }
    
}
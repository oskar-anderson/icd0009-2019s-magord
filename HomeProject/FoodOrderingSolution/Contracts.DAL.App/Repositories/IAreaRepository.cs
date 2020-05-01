using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{

    public interface IAreaRepository : IBaseRepository<Area>, IAreaRepositoryCustom
    {
        
    }

    public interface IAreaRepositoryCustom
    {
        
    }
    
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IChoiceRepository : IBaseRepository<Choice>
    {
        Task<IEnumerable<ChoiceView>> GetAllForViewAsync(Guid questionId, object? userId = null, bool noTracking = true);
    }
}
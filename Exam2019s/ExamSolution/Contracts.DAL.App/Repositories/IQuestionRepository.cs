using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.App.DTO;
using ee.itcollege.magord.healthyfood.Contracts.DAL.Base.Repositories;

namespace Contracts.DAL.App.Repositories
{
    public interface IQuestionRepository : IBaseRepository<Question>
    {
        Task<IEnumerable<QuestionView>> GetAllForViewAsync(Guid quizId, object? userId = null, bool noTracking = true);
        
    }
}
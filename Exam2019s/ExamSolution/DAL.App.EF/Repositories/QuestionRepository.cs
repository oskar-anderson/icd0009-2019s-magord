using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using ee.itcollege.magord.healthyfood.DAL.Base.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class QuestionRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Question, DAL.App.DTO.Question>, IQuestionRepository
    {
        public QuestionRepository(AppDbContext dbContext) : base (dbContext, new BaseMapper<Domain.App.Question, DAL.App.DTO.Question>())
        {
            
        }


        public virtual async Task<IEnumerable<QuestionView>> GetAllForViewAsync(Guid quizId, object? userId = null, bool noTracking = true)
        {
            return await RepoDbSet
                .Include(c => c.Quiz)
                .Where(c => c.QuizId == quizId)
                .Select(c => new QuestionView()
                {
                    Id = c.Id,
                    Number = c.Number,
                    Description = c.Description,
                    Points = c.Points,
                    Quiz = c.Quiz!.Name
                }).ToListAsync();
        }
    }
}
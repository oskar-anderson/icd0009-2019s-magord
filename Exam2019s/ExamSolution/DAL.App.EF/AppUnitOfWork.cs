using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using ee.itcollege.magord.healthyfood.DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IQuestionRepository Questions =>
            GetRepository<IQuestionRepository>(() => new QuestionRepository(UowDbContext));
        
        public IQuizRepository Quizzes =>
            GetRepository<IQuizRepository>(() => new QuizRepository(UowDbContext));
        
        public IChoiceRepository Choices =>
            GetRepository<IChoiceRepository>(() => new ChoiceRepository(UowDbContext));
        
        public IResultRepository Results =>
            GetRepository<IResultRepository>(() => new ResultRepository(UowDbContext));

    }
}
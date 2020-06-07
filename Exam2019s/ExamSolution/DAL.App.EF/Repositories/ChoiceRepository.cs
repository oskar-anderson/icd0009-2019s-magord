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
    public class ChoiceRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Choice, DAL.App.DTO.Choice>, IChoiceRepository
    {
        public ChoiceRepository(AppDbContext dbContext) : base (dbContext, new BaseMapper<Domain.App.Choice, DAL.App.DTO.Choice>())
        {
        }
        
        

        /*
        public virtual async Task<IEnumerable<DTO.Course>> GetAllForViewAsync(Guid? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }
        */
        public virtual async Task<IEnumerable<ChoiceView>> GetAllForViewAsync(Guid questionId, object? userId = null, bool noTracking = true)
        {
            return await RepoDbSet
                .Where(c => c.QuestionId == questionId)
                .Select(c => new ChoiceView()
                {
                    Id = c.Id,
                    IsAnswer = c.IsAnswer,
                    IsSelected = c.IsSelected,
                    Value = c.Value,
                    QuestionId = c.QuestionId
                }).ToListAsync();
        }
    }
}
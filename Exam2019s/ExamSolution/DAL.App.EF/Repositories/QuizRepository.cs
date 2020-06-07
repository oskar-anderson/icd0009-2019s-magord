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
    public class QuizRepository : 
        EFBaseRepository<AppDbContext, Domain.App.Identity.AppUser, Domain.App.Quiz, DAL.App.DTO.Quiz>, IQuizRepository
    {
        public QuizRepository(AppDbContext dbContext) : base (dbContext, new BaseMapper<Domain.App.Quiz, DAL.App.DTO.Quiz>())
        {
            
        }
        
    }
}
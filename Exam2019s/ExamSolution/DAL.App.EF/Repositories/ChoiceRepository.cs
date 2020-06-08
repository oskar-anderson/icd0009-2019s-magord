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
        
    }
}
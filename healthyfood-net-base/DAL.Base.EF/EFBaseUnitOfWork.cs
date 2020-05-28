using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ee.itcollege.magord.healthyfood.DAL.Base.EF
{
    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey>
        where TDbContext : DbContext
        where TKey : IEquatable<TKey>

    {
        protected readonly TDbContext UowDbContext;

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
            var result =await UowDbContext.SaveChangesAsync();
            UpdateTrackedEntities();
            return result;
        }
    }
}
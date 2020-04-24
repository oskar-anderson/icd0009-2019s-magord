using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TDomainEntity, TDALEntity> : EFBaseRepository<Guid, TDbContext, TDomainEntity, TDALEntity>,
        IBaseRepository<TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<Guid>, new()
        where TDomainEntity : class, IDomainEntityBaseMetadata<Guid>, new()
        where TDbContext : DbContext
    {
        public EFBaseRepository(TDbContext dbContext,  IBaseDALMapper<TDomainEntity, TDALEntity> mapper) : base(dbContext, mapper)
        {
        }
    }
    
    public class EFBaseRepository<TKey, TDbContext, TDomainEntity, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainBaseEntity<TKey>, new()
        where TDomainEntity : class, IDomainEntityBaseMetadata<TKey>, new()
        where TKey : IEquatable<TKey>
        where TDbContext : DbContext

    {
        protected TDbContext RepoDbContext;
        protected DbSet<TDomainEntity> RepoDbSet;
        protected IBaseDALMapper<TDomainEntity, TDALEntity> Mapper;

        public EFBaseRepository(TDbContext dbContext, IBaseDALMapper<TDomainEntity, TDALEntity> mapper)
        {
            RepoDbContext = dbContext;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>();
            Mapper = mapper;

            if (RepoDbSet == null)
            {
                throw new ArgumentNullException(typeof(TDALEntity).Name + " was not found as DBSet!");
            }
        }

        public virtual IEnumerable<TDALEntity> All()
        {
            return RepoDbSet.ToList().Select(domainEntity => Mapper.Map(domainEntity));
        }

        public virtual async Task<IEnumerable<TDALEntity>> AllAsync()
        {
            return (await RepoDbSet.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public virtual TDALEntity Find(params object[] id)
        {
            return Mapper.Map(RepoDbSet.Find(id));
        }

        public virtual async Task<TDALEntity> FindAsync(params object[] id)
        {
            return Mapper.Map(await RepoDbSet.FindAsync(id));
        }

        public virtual TDALEntity Add(TDALEntity entity)
        {
            return Mapper.Map(RepoDbSet.Add(Mapper.Map<TDALEntity, TDomainEntity>(entity)).Entity);
        }

        public virtual TDALEntity Update(TDALEntity entity)
        {
            return Mapper.Map(RepoDbSet.Update(Mapper.Map<TDALEntity, TDomainEntity>(entity)).Entity);
        }

        public virtual TDALEntity Remove(TDALEntity entity)
        {
            return Mapper.Map(RepoDbSet.Remove(Mapper.Map<TDALEntity, TDomainEntity>(entity)).Entity);
        }

        public virtual TDALEntity Remove(params object[] id)
        {
            return  Mapper.Map(RepoDbSet.Remove(RepoDbSet.Find(id)).Entity);
        }
    }
}
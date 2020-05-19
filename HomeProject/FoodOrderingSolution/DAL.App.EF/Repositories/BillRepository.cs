using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class BillRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Bill, DAL.App.DTO.Bill>, IBillRepository
    {
        public BillRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.Bill, DAL.App.DTO.Bill>())
        {
        }

        public override async Task<IEnumerable<Bill>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(b => b.Person)
                .Include(b => b.Order)
                .Include(b => b.AppUser);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Bill> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                    .Include(b => b.Person)
                    .Include(b => b.Order)
                    .Include(b => b.AppUser)
                    .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<BillView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(b => b.Person)
                .Include(b => b.Order)
                .Select(a => new BillView()
                {
                    Id = a.Id,
                    Number = a.Number,
                    Sum = a.Sum,
                    TimeIssued = a.TimeIssued,
                    Order = a.Order!.Number,
                    Person = a.Person!.FirstName,
                }).ToListAsync();
        }

        public virtual async Task<BillView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(b => b.Person)
                .Include(b => b.Order)
                .Where(r => r.Id == id)
                .Select(a => new BillView()
                {
                    Id = a.Id,
                    Number = a.Number,
                    Sum = a.Sum,
                    TimeIssued = a.TimeIssued,
                    Order = a.Order!.Number,
                    Person = a.Person!.FirstName,
                })
                .FirstOrDefaultAsync();
        }

        /*
        public async Task<IEnumerable<BillDTO>> DTOAllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Person)
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(b => b.Order!.AppUserId == userId);
            }
            return await query
                .Select(b => new BillDTO()
                {
                    Id = b.Id,
                    TimeIssued = b.TimeIssued,
                    Number = b.Number,
                    Sum = b.Sum,
                    OrderId = b.OrderId,
                    PersonId = b.PersonId,
                    Order = new OrderDTO()
                    {
                        Id = b.Order!.Id,
                        OrderStatus = b.Order.OrderStatus,
                        TimeCreated = b.Order.TimeCreated,
                        Number = b.Order.Number,
                        IngredientId = b.Order.IngredientId,
                        FoodId = b.Order.FoodId,
                        PersonId = b.Order.PersonId,
                        RestaurantId = b.Order.RestaurantId,
                        OrderTypeId = b.Order.OrderTypeId,
                        DrinkId = b.Order.DrinkId
                    },
                    Person = new PersonDTO()
                    {
                        Id = b.Person!.Id,
                        DateOfBirth = b.Person.DateOfBirth,
                        FirstName = b.Person.FirstName,
                        LastName = b.Person.LastName,
                        Sex = b.Person.Sex
                    }
                }).ToListAsync();
        }
        
        public async Task<BillDTO> DTOFirstOrDefaultAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Person)
                .Where(b => b.Id == id).AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(b => b.Order!.AppUserId == userId);
            }

            var billDTO = await query.Select(b => new BillDTO()
            {
                Id = b.Id,
                TimeIssued = b.TimeIssued,
                Number = b.Number,
                Sum = b.Sum,
                OrderId = b.OrderId,
                PersonId = b.PersonId,
                Order = new OrderDTO()
                {
                    Id = b.Order!.Id,
                    OrderStatus = b.Order.OrderStatus,
                    TimeCreated = b.Order.TimeCreated,
                    Number = b.Order.Number,
                    IngredientId = b.Order.IngredientId,
                    FoodId = b.Order.FoodId,
                    PersonId = b.Order.PersonId,
                    RestaurantId = b.Order.RestaurantId,
                    OrderTypeId = b.Order.OrderTypeId,
                    DrinkId = b.Order.DrinkId
                },
                Person = new PersonDTO()
                {
                    Id = b.Person!.Id,
                    DateOfBirth = b.Person.DateOfBirth,
                    FirstName = b.Person.FirstName,
                    LastName = b.Person.LastName,
                    Sex = b.Person.Sex
                }
            }).FirstOrDefaultAsync();

            return billDTO;
        }
        */
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;


namespace DAL.App.EF.Repositories
{
    public class BillRepository : EFBaseRepository<AppDbContext, Domain.Bill, DAL.App.DTO.Bill>, IBillRepository
    {
        public BillRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Bill, DAL.App.DTO.Bill>())
        {
        }
        
        public async Task<IEnumerable<DAL.App.DTO.Bill>> AllAsync(Guid? userId = null)
        {
            var query = RepoDbSet
                .Include(b => b.Person)
                .Include(b => b.Order)
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(b => b.Order!.AppUserId == userId && b.Person!.AppUserId == userId);
            }

            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }

        public async Task<DAL.App.DTO.Bill> FirstOrDefaultAsync(Guid id, Guid? userId = null)
        {

            var query = RepoDbSet
                .Include(b => b.Order)
                .Include(b => b.Person)
                .Where(b => b.Id == id)
                .AsQueryable();
            
            if (userId != null)
            {
                query = query.Where(b => b.Order!.AppUserId == userId && b.Person!.AppUserId == userId);
            }

            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id, Guid? userId = null)
        {
            if (userId == null)
            {
                return await RepoDbSet.AnyAsync(b => b.Id == id);
            }

            return await RepoDbSet.AnyAsync(b => b.Id == id && b.Order!.AppUserId == userId);
        }

        public async Task DeleteAsync(Guid id, Guid? userId = null)
        {
            var query = RepoDbSet.Where(a => a.Id == id).AsQueryable();
            if (userId != null)
            {
                query = query.Where(a => a.AppUserId == userId);
            }

            var bill = await query.AsNoTracking().FirstOrDefaultAsync();
            base.Remove(bill.Id);
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
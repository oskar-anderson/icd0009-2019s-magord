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
    public class PaymentRepository : EFBaseRepository<AppDbContext, Domain.Identity.AppUser, Domain.Payment, DAL.App.DTO.Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext,
            new DALMapper<Domain.Payment, DAL.App.DTO.Payment>())
        {
        }

        public override async Task<IEnumerable<Payment>> GetAllAsync(object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(r => r.Person)
                .Include(r => r.Bill)
                .Include(r => r.PaymentType);
            var domainEntities = await query.ToListAsync();
            var result = domainEntities.Select(e => Mapper.Map(e));
            return result;
        }

        public override async Task<Payment> FirstOrDefaultAsync(Guid id, object? userId = null, bool noTracking = true)
        {
            var query = PrepareQuery(userId, noTracking);
            query = query
                .Include(r => r.Person)
                .Include(r => r.Bill)
                .Include(r => r.PaymentType)
                .Where(r => r.Id == id);
            var domainEntity = await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
            var result = Mapper.Map(domainEntity);
            return result;
        }
        
        public virtual async Task<IEnumerable<PaymentView>> GetAllForViewAsync()
        {
            return await RepoDbSet
                .Include(r => r.Person)
                .Include(r => r.Bill)
                .Include(r => r.PaymentType)
                .Select(a => new PaymentView()
                {
                    Id = a.Id,
                    Amount = a.Amount,
                    TimeMade = a.TimeMade,
                    Person = a.Person!.FirstName,
                    Bill = a.Bill!.Number,
                    PaymentType = a.PaymentType!.Name,
                }).ToListAsync();
        }

        public virtual async Task<PaymentView> FirstOrDefaultForViewAsync(Guid id)
        {
            return await RepoDbSet
                .Include(r => r.Person)
                .Include(r => r.Bill)
                .Include(r => r.PaymentType)
                .Where(r => r.Id == id)
                .Select(a => new PaymentView()
                {
                    Id = a.Id,
                    Amount = a.Amount,
                    TimeMade = a.TimeMade,
                    Person = a.Person!.FirstName,
                    Bill = a.Bill!.Number,
                    PaymentType = a.PaymentType!.Name,
                })
                .FirstOrDefaultAsync();
        }

        /*
        public async Task<IEnumerable<PaymentDTO>> DTOAllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Bill)
                .Include(p => p.PaymentType)
                .AsQueryable();
            
            return await query
                .Select(p => new PaymentDTO()
                {
                    Id = p.Id,
                    Amount = p.Amount,
                    TimeMade = p.TimeMade,
                    PaymentTypeId = p.PaymentTypeId,
                    PersonId = p.PersonId,
                    BillId = p.BillId,
                    PaymentType = new PaymentTypeDTO()
                    {
                        Id = p.PaymentType!.Id,
                        Name = p.PaymentType.Name
                    },
                    Bill = new BillDTO()
                    {
                        Id = p.Bill!.Id,
                        Number = p.Bill.Number,
                        OrderId = p.Bill.OrderId,
                        PersonId = p.Bill.PersonId,
                        Sum = p.Bill.Sum,
                        TimeIssued = p.Bill.TimeIssued,
                    },
                    Person = new PersonDTO()
                    {
                        Id = p.Person!.Id,
                        DateOfBirth = p.Person.DateOfBirth,
                        FirstName = p.Person.FirstName,
                        LastName = p.Person.LastName,
                        Sex = p.Person.Sex
                    }
                })
                .ToListAsync();
        }

        public async Task<PaymentDTO> DTOFirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Bill)
                .Include(p => p.PaymentType)
                .Where(p => p.Id == id).AsQueryable();

            var paymentDTO = await query.Select(p => new PaymentDTO()
            {
                Id = p.Id,
                Amount = p.Amount,
                TimeMade = p.TimeMade,
                PaymentTypeId = p.PaymentTypeId,
                PersonId = p.PersonId,
                BillId = p.BillId,
                PaymentType = new PaymentTypeDTO()
                {
                    Id = p.PaymentType!.Id,
                    Name = p.PaymentType.Name
                },
                Bill = new BillDTO()
                {
                    Id = p.Bill!.Id,
                    Number = p.Bill.Number,
                    OrderId = p.Bill.OrderId,
                    PersonId = p.Bill.PersonId,
                    Sum = p.Bill.Sum,
                    TimeIssued = p.Bill.TimeIssued,
                },
                Person = new PersonDTO()
                {
                    Id = p.Person!.Id,
                    DateOfBirth = p.Person.DateOfBirth,
                    FirstName = p.Person.FirstName,
                    LastName = p.Person.LastName,
                    Sex = p.Person.Sex
                }
            }).FirstOrDefaultAsync();

            return paymentDTO;
        }
        */
    }
}
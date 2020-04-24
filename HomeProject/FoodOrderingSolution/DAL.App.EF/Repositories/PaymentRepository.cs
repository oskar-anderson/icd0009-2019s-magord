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
    public class PaymentRepository : EFBaseRepository<AppDbContext, Domain.Payment, DAL.App.DTO.Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext,
            new BaseDALMapper<Domain.Payment, DAL.App.DTO.Payment>())
        {
        }
        
        
        public new async Task<IEnumerable<DAL.App.DTO.Payment>> AllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Bill)
                .Include(p => p.PaymentType)
                .AsQueryable();
            
            return (await query.ToListAsync()).Select(domainEntity => Mapper.Map(domainEntity));
        }


        public async Task<DAL.App.DTO.Payment> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Bill)
                .Include(p => p.PaymentType)
                .Where(p => p.Id == id)
                .AsQueryable();
            
            return Mapper.Map(await query.FirstOrDefaultAsync());
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await RepoDbSet.AnyAsync(a => a.Id == id);
        }

        public async Task DeleteAsync(Guid id)
        {
            var payment = await FirstOrDefaultAsync(id);
            base.Remove(payment);
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
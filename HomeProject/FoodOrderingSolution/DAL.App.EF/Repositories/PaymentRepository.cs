using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.PaymentDTOs;
using PublicApi.DTO.v1.PaymentTypeDTOs;

namespace DAL.App.EF.Repositories
{
    public class PaymentRepository : EFBaseRepository<Payment, AppDbContext>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        
        
        public new async Task<IEnumerable<Payment>> AllAsync()
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Bill)
                .Include(p => p.PaymentType)
                .AsQueryable();
            
            return await query.ToListAsync();
        }


        public async Task<Payment> FirstOrDefaultAsync(Guid id)
        {
            var query = RepoDbSet
                .Include(p => p.Person)
                .Include(p => p.Bill)
                .Include(p => p.PaymentType)
                .Where(p => p.Id == id)
                .AsQueryable();
            
            return await query.FirstOrDefaultAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var payment = await FirstOrDefaultAsync(id);
            base.Remove(payment);
        }
        
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
    }
}
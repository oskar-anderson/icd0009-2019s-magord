using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PaymentTypeRepository : EFBaseRepository<PaymentType, AppDbContext>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
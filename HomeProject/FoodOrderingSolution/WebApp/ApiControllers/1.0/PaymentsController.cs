using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.PaymentDTOs;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDTO>>> GetPayments()
        {
            var paymentDTOs = await _uow.Payments.DTOAllAsync();
            
            return Ok(paymentDTOs);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDTO>> GetPayment(Guid id)
        {
            var payment = await _uow.Payments.DTOFirstOrDefaultAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(Guid id, PaymentEditDTO paymentEditDTO)
        {
            if (id != paymentEditDTO.Id)
            {
                return BadRequest();
            }

            var payment = await _uow.Payments.FirstOrDefaultAsync(paymentEditDTO.Id);
            if (payment == null)
            {
                return BadRequest();
            }

            payment.Amount = paymentEditDTO.Amount;
            
            _uow.Payments.Update(payment);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Payments.ExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(PaymentCreateDTO paymentCreateDTO)
        {
            var payment = new Payment
            {
                Id = paymentCreateDTO.Id,
                Amount = paymentCreateDTO.Amount,
                TimeMade = paymentCreateDTO.TimeMade
            };
            
            _uow.Payments.Add(payment);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = payment.Id }, payment);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Payment>> DeletePayment(Guid id)
        {
            var payment = await _uow.Payments.FirstOrDefaultAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _uow.Payments.Remove(payment);
            await _uow.SaveChangesAsync();

            return Ok(payment);
        }
    }
}

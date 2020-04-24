using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1.PaymentTypeDTOs;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentTypesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/PaymentTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentTypeDTO>>> GetPaymentTypes()
        {
            var paymentTypeDTOs = await _uow.PaymentTypes.DTOAllAsync();
            
            return Ok(paymentTypeDTOs);
        }

        // GET: api/PaymentTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentTypeDTO>> GetPaymentType(Guid id)
        {
            var paymentType = await _uow.PaymentTypes.DTOFirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return Ok(paymentType);
        }

        // PUT: api/PaymentTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType(Guid id, PaymentTypeEditDTO paymentTypeEditDTO)
        {
            if (id != paymentTypeEditDTO.Id)
            {
                return BadRequest();
            }

            var paymentType = await _uow.PaymentTypes.FirstOrDefaultAsync(paymentTypeEditDTO.Id);
            if (paymentType == null)
            {
                return BadRequest();
            }

            paymentType.Name = paymentTypeEditDTO.Name;
            
            _uow.PaymentTypes.Update(paymentType);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.PaymentTypes.ExistsAsync(id))
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

        // POST: api/PaymentTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PaymentType>> PostPaymentType(PaymentTypeCreateDTO paymentTypeCreateDTO)
        {
            var paymentType = new PaymentType
            {
                Id = paymentTypeCreateDTO.Id,
                Name = paymentTypeCreateDTO.Name,
            };
            
            _uow.PaymentTypes.Add(paymentType);
            await _uow.SaveChangesAsync();
            
            return CreatedAtAction("GetPaymentType", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentType>> DeletePaymentType(Guid id)
        {
            var paymentType = await _uow.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _uow.PaymentTypes.Remove(paymentType);
            await _uow.SaveChangesAsync();

            return Ok(paymentType);
        }
    }
}

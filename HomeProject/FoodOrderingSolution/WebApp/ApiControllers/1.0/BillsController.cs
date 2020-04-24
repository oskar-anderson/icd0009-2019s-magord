using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public BillsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillDTO>>> GetBills()
        {
            var billDTOs = await _uow.Bills.DTOAllAsync(User.UserGuidId());

            return Ok(billDTOs);
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BillDTO>> GetBill(Guid id)
        {
            var bill = await _uow.Bills.DTOFirstOrDefaultAsync(id, User.UserGuidId());

            if (bill == null)
            {
                return NotFound();
            }

            return Ok(bill);
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(Guid id, BillEditDTO billEditDTO)
        {
            if (id != billEditDTO.Id)
            {
                return BadRequest();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(billEditDTO.Id, User.UserGuidId());
            if (bill == null)
            {
                return BadRequest();
            }
            
            bill.TimeIssued = billEditDTO.TimeIssued;
            bill.Number = billEditDTO.Number;
            bill.Sum = billEditDTO.Sum;
            
            _uow.Bills.Update(bill);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Bills.ExistsAsync(id, User.UserGuidId()))
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

        // POST: api/Bills
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(BillCreateDTO billCreateDTO)
        {
            var bill = new Bill
            {
                Id = billCreateDTO.Id,
                AppUserId = User.UserGuidId(),
                TimeIssued = billCreateDTO.TimeIssued,
                Number = billCreateDTO.Number,
                Sum = billCreateDTO.Sum
            };

            _uow.Bills.Add(bill);
            await _uow.SaveChangesAsync();
            
            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bill>> DeleteBill(Guid id)
        {
            var bill = await _uow.Bills.FirstOrDefaultAsync(id, User.UserGuidId());
            if (bill == null)
            {
                return NotFound();
            }

            _uow.Bills.Remove(bill);
            await _uow.SaveChangesAsync();

            return Ok(bill);

        }
    }
}

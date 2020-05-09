using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1.Mappers;
using V1DTO=PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Bills Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class BillsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly BillMapper _mapper = new BillMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public BillsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get a list of all the Bill-s
        /// </summary>
        /// <returns>List of Bills</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.Bill>))]
        public async Task<ActionResult<IEnumerable<V1DTO.Bill>>> GetBills()
        {
            return Ok((await _bll.Bills.GetAllAsync()).Select(e => _mapper.Map(e)));
        }

        /// <summary>
        /// Get a single Bill
        /// </summary>
        /// <param name="id">Bill Id</param>
        /// <returns>Bill object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Bill))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Bill>> GetBill(Guid id)
        {
            var bill = await _bll.Bills.FirstOrDefaultAsync(id);
            
            if (bill == null)
            {
                return NotFound(new {message = "Bill not found"});
            }

            return Ok(_mapper.Map(bill));
        }

        /// <summary>
        /// Update the Bill
        /// </summary>
        /// <param name="id">Bill id</param>
        /// <param name="bill">Bill object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutBill(Guid id, V1DTO.Bill bill)
        {
            bill.AppUserId = User.UserId();
            
            if (id != bill.Id)
            {
                return BadRequest(new {message = "The id and bill.id do not match!"});
            }

            await _bll.Bills.UpdateAsync(_mapper.Map(bill), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Bill 
        /// </summary>
        /// <param name="bill">Bill object to create</param>
        /// <returns>Created Bill object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Bill))]
        public async Task<ActionResult<V1DTO.Bill>> PostBill(V1DTO.Bill bill)
        {
            bill.AppUserId = User.UserId();

            var bllEntity = _mapper.Map(bill);
            _bll.Bills.Add(bllEntity);
            await _bll.SaveChangesAsync();
            bill.Id = bllEntity.Id;
            
            return CreatedAtAction("GetBill",
                new { id = bill.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                bill);
        }

        /// <summary>
        /// Delete a Bill
        /// </summary>
        /// <param name="id">Bill Id</param>
        /// <returns>Deleted Bill object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Bill))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Bill>> DeleteBill(Guid id)
        {
            var bill = await _bll.Bills.FirstOrDefaultAsync(id, User.UserId());
            if (bill == null)
            {
                return NotFound(new {message = "Bill not found"});
            }

            await _bll.Bills.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(bill);
        }
    }
}

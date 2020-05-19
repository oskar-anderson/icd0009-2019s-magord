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
    /// Payments Api Controller
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMapper _mapper = new PaymentMapper();
        
        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        /// <summary>
        /// Get a list of all the Payment-s
        /// </summary>
        /// <returns>List of Payments</returns>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<V1DTO.PaymentView>))]
        public async Task<ActionResult<IEnumerable<V1DTO.PaymentView>>> GetPayments()
        {
            return Ok((await _bll.Payments.GetAllForViewAsync()).Select(e => _mapper.MapPaymentView(e)));
        }

        /// <summary>
        /// Get a single Payment
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns>Payment object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Payment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Payment>> GetPayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultForViewAsync(id);
            
            if (payment == null)
            {
                return NotFound(new {message = "Payment not found"});
            }

            return Ok(_mapper.MapPaymentView(payment));
        }

        /// <summary>
        /// Update the Payment
        /// </summary>
        /// <param name="id">Payment id</param>
        /// <param name="payment">Payment object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutPayment(Guid id, V1DTO.Payment payment)
        {
            payment.AppUserId = User.UserId();
            
            if (id != payment.Id)
            {
                return BadRequest(new {message = "The id and payment.id do not match!"});
            }

            await _bll.Payments.UpdateAsync(_mapper.Map(payment), User.UserId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Create a new Payment 
        /// </summary>
        /// <param name="payment">Payment object to create</param>
        /// <returns>Created Payment object</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(V1DTO.Payment))]
        public async Task<ActionResult<V1DTO.Payment>> PostPayment(V1DTO.Payment payment)
        {
            payment.AppUserId = User.UserId();
            payment.TimeMade = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var bllEntity = _mapper.Map(payment);
            _bll.Payments.Add(bllEntity);
            await _bll.SaveChangesAsync();
            payment.Id = bllEntity.Id;
            
            return CreatedAtAction("GetPayment",
                new { id = payment.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0" },
                payment);
        }

        /// <summary>
        /// Delete an Payment
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns>Deleted Payment object</returns>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(V1DTO.Payment))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<V1DTO.Payment>> DeletePayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id, User.UserId());
            if (payment == null)
            {
                return NotFound(new {message = "Payment not found"});
            }

            await _bll.Payments.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(payment);
        }
    }
}

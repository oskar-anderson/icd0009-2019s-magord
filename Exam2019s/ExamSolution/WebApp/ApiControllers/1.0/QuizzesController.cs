using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QuizzesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public QuizzesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Quizzes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DAL.App.DTO.Quiz>>> GetQuizzes()
        {
            return Ok(await _uow.Quizzes.GetAllAsync());
        }

        // GET: api/Quizzes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DAL.App.DTO.Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // PUT: api/Quizzes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutQuiz(Guid id, DAL.App.DTO.Quiz quiz)
        {
            quiz.AppUserId = User.UserId();
            
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            await _uow.Quizzes.UpdateAsync(quiz);
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Quizzes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Quiz>> PostQuiz(DAL.App.DTO.Quiz quiz)
        {
            quiz.AppUserId = User.UserId();
            
            _uow.Quizzes.Add(quiz);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetQuiz",
                new {id = quiz.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, quiz);
        }

        // DELETE: api/Quizzes/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Quiz>> DeleteQuiz(Guid id)
        {
            var quiz = await _uow.Quizzes.FirstOrDefaultAsync(id);
            
            if (quiz == null)
            {
                return NotFound();
            }

            await _uow.Quizzes.RemoveAsync(quiz);
            await _uow.SaveChangesAsync();

            return Ok(quiz);
        }
    }
}
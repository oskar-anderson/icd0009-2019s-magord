using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers._1._0
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuestionsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public QuestionsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Questions
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DAL.App.DTO.QuestionView>>> GetQuestions(Guid quizId)
        {
            return Ok(await _uow.Questions.GetAllForViewAsync(quizId));
        }

        // GET: api/Questions/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DAL.App.DTO.Question>> GetQuestion(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // PUT: api/Questions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<IActionResult> PutQuestion(Guid id, DAL.App.DTO.Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }

            await _uow.Questions.UpdateAsync(question);
            await _uow.SaveChangesAsync();


            return NoContent();
        }

        // POST: api/Questions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<Question>> PostQuestion(DAL.App.DTO.Question question)
        {
            var questions = await _uow.Questions.GetAllForViewAsync(question.QuizId);

            question.Number = questions.Count() + 1;
            
            _uow.Questions.Add(question);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetQuestion",
                new {id = question.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"}, question);
        }

        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<DAL.App.DTO.Question>> DeleteQuestion(Guid id)
        {
            var question = await _uow.Questions.FirstOrDefaultAsync(id);
            
            if (question == null)
            {
                return NotFound();
            }

            await _uow.Questions.RemoveAsync(question);
            await _uow.SaveChangesAsync();

            return Ok(question);
        }
    }
}
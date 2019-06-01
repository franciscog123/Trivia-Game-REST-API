using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library.Models;

namespace TriviaGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizQuestionController : ControllerBase
    {
        public IQuizQuestionRepository QQRepo { get; set; }

        public QuizQuestionController(IQuizQuestionRepository qqRepo)
        {
            QQRepo = qqRepo;
        }

        /*
        // GET: api/QuizQuestion
        [HttpGet]
        public IEnumerable<string> GetQuizQuestion()
        {
            return new string[] { "value1", "value2" };
        }*/
        
        
        // GET: api/QuizQuestion/5
        [HttpGet("{id}", Name = "GetQuizQuestion")]
        public async Task<ActionResult<QuizQuestion>> GetQuizQuestion(int id)
        {
            if(await QQRepo.GetQuizQuestionById(id)is QuizQuestion quizQuestion)
            {
                return quizQuestion;
            }
            return NotFound();
        }

        // POST: api/QuizQuestion
        [HttpPost]
        public async Task <IActionResult>Post([FromBody] QuizQuestion quizQuestion)
        {
            try
            {
                var id = await QQRepo.CreateQuizQuestion(quizQuestion);
                QuizQuestion model = await QQRepo.GetQuizQuestionById(id);
                return CreatedAtRoute("Get", new { Id = id }, model);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*
        // PUT: api/QuizQuestion/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.DataAccess.Repositories;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library.Models;

namespace TriviaGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        public IUserRepository UserRepo { get; set; }
        public IQuizRepository QuizRepo { get; set; }
        public IQuestionRepository QuestionRepo { get; set; }
        public ICategoryRepository CategoryRepo { get; set; }

        public QuestionController(IUserRepository userRepo, IQuizRepository quizRepo, IQuestionRepository questionRepo, ICategoryRepository catRepo)
        {
            UserRepo = userRepo;
            QuizRepo = quizRepo;
            QuestionRepo = questionRepo;
            CategoryRepo = catRepo;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> Get()
        {
            var questions= await QuestionRepo.GetQuestions();
            if(questions==null)
            {
                return NotFound();
            }
            return Ok(questions);
        }

        // GET: api/Question/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Question>> Get(int id)
        {
            if(await QuestionRepo.GetQuestionById(id) is Question question)
            {
             return question;
            }
            return NotFound();
        }
        [HttpGet]
        [Route("GetQuestionsByCategory/{id}")]
        public IEnumerable<Question> GetQuestionsByCategory(int id)
        {
            return QuestionRepo.GetQuestionsByCategoryId(id);
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var items = await CategoryRepo.GetCategories();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet]
        [Route("GetLastQuestion")]
        public async Task<ActionResult<int>> GetLastQuestionAdded()
        {
            var lastQuestion= await QuestionRepo.GetLastQuestionAdded();
            if (lastQuestion!=0)
            {
                return Ok(lastQuestion);
            }
            return NotFound();
        }
        
        // POST: api/Question
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Question question)
        {
            try
            {
                var id = await QuestionRepo.CreateQuestion(question);

                Question model = await QuestionRepo.GetQuestionById(id);

                return CreatedAtRoute("Get", new { Id = id }, model);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await QuestionRepo.DeleteQuestion(id);
            if(!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

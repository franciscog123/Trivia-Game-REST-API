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
    public class QuizController : ControllerBase
    {

        public IUserRepository UserRepo { get; set; }
        public IQuizRepository QuizRepo { get; set; }
        public ICategoryRepository CategoryRepo { get; set; }

        public QuizController(IUserRepository userRepo, IQuizRepository quizRepo, ICategoryRepository catRepo)
        {
            UserRepo = userRepo;
            QuizRepo = quizRepo;
            CategoryRepo = catRepo;
        }

        // GET: api/Quiz
        [HttpGet]
        public IEnumerable<Quiz> GetQuizzes()
        {
            var quizzes = QuizRepo.GetQuizzes();
            return quizzes;
        }

        //GET: api/quiz/getquizzesbygamemode/1
        [HttpGet]
        [Route("GetQuizzesByGameMode/{id}")]
        public IEnumerable<Quiz> GetQuizzesByGameMode(int id)
        {
            var quizzes = QuizRepo.GetQuizzesByGameModeId(id);
            return quizzes;
        }

       
        // GET: api/Quiz/5
        [HttpGet("{id}", Name = "GetQuizById")]
        public async Task<ActionResult<Quiz>> GetQuizById(int id)
        {

            if(await QuizRepo.GetQuizById(id) is Quiz quiz)
            {
                return quiz;
            }
            return NotFound();
        }

        // GET: api/Quiz/5
        [HttpGet]
        [Route("GetLastQuizId")]
        public int GetLastQuizId()
        {
            return QuizRepo.GetLastQuizId();
        }

        //GET: api/quiz/getquizzesbyuser/1
        [HttpGet]
        [Route("GetQuizzesByUser/{id}")]
        public IEnumerable<Quiz> GetQuizzesByUserId(int id)
        {
            var quizzes = QuizRepo.GetQuizzesByUserId(id);
            return quizzes;
        }

        [HttpGet]
        [Route("GetCategory/{id}")]
        public async Task<ActionResult<string>> GetCategory(int id)
        {
            var category = await CategoryRepo.GetCategoryById(id);
            {
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            }
        }

        //GET: api/quiz/getquizzesbyuser/1
        [HttpGet]
        [Route("GetQuizzesByCategory/{id}")]
        public IEnumerable<Quiz> GetQuizzesByCategoryId(int id)
        {
            var quizzes = QuizRepo.GetQuizzesByCategoryId(id);
            return quizzes;
        }

        //haven't used this, might delete later
        [HttpGet]
        [Route("CalcTotalScoreByUser/{id}")]
        public async Task<ActionResult<int>> CalcTotalScoreByUser(int id)
        {
            var total = QuizRepo.CalcTotalScoreByUser(id);
            if(await total>=0)
            {
                return Ok(total);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("GetRandomQuestion/{categoryId}")]
        public async Task<ActionResult<Question>> GetRandomQuestion(int categoryId)
        {
            var result= await QuizRepo.GetRandomQuestion(categoryId);
            if(result is null)
            {
                return NotFound();
            }
            return result;
        }

        // POST: api/Quiz
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Quiz quiz)
        {
            try
            {
                var id = await QuizRepo.CreateQuiz(quiz);
                Quiz model = await QuizRepo.GetQuizById(id);

                return CreatedAtRoute("Get", new { Id = id }, model);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Quiz quiz)
        {
            try
            {
                Quiz existing = await QuizRepo.GetQuizById(id);
                if(existing is null)
                {
                    return NotFound();
                }
                quiz.QuizId = id;

                var success = await QuizRepo.EditQuiz(quiz);
                if(!success)
                {
                    return BadRequest("invalid request");
                }
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();
        }
    }
}

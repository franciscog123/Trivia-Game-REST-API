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

        public QuizController(IUserRepository userRepo, IQuizRepository quizRepo)
        {
            UserRepo = userRepo;
            QuizRepo = quizRepo;
        }

        // GET: api/Quiz
        [HttpGet]
        public IEnumerable<Quiz> GetQuizzes()
        {
            var quizzes = QuizRepo.GetQuizzes();
            return quizzes;
            //return new string[] { "value1", "value2" };
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
        public Quiz GetQuizById(int id)
        {
            return QuizRepo.GetQuizById(id);
            //return "value";
        }

        // GET: api/Quiz/5
        [HttpGet]
        [Route("GetLastQuizId")]
        public int GetLastQuizId()
        {
            return QuizRepo.GetLastQuizId();
            //return "value";
        }

        //GET: api/quiz/getquizzesbyuser/1
        [HttpGet]
        [Route("GetQuizzesByUser/{id}")]
        public IEnumerable<Quiz> GetQuizzesByUserId(int id)
        {
            var quizzes = QuizRepo.GetQuizzesByUserId(id);
            return quizzes;
        }

        //GET: api/quiz/getquizzesbyuser/1
        [HttpGet]
        [Route("GetQuizzesByCategory/{id}")]
        public IEnumerable<Quiz> GetQuizzesByCategoryId(int id)
        {
            var quizzes = QuizRepo.GetQuizzesByCategoryId(id);
            return quizzes;
        }

        //Todo: haven't used this, might delete later
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

        // POST: api/Quiz
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Quiz/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

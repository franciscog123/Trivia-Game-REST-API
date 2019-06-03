using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library;
using TriviaGame.Library.Models;

namespace TriviaGame.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserRepository UserRepo { get; set; }
        public IQuizRepository QuizRepo { get; set; }

        public UserController(IUserRepository userRepo, IQuizRepository quizRepo)
        {
            UserRepo = userRepo;
            QuizRepo = quizRepo;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        { 
            var users= await UserRepo.GetUsers();
            //return result;
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }
        //GET: api/user/getscoreboards
        [HttpGet]
        [Route("GetScoreBoards")]
        public IEnumerable<ScoreBoard> GetScoreBoards()
        {
            return UserRepo.GetAllScoreboards();
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        // GET: api/User/5
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var user = await UserRepo.GetUserByEmail(email);
            if(user==null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // GET: api/getUserbyid/5
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            if(await UserRepo.GetUserAsync(id)is User user)
            {
                return user;
            }
            return NotFound();
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                var id = await UserRepo.AddUser(user);
                User model = await UserRepo.GetUserAsync(id);
                return CreatedAtRoute("Get", new { Id = id }, model);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/User/5
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

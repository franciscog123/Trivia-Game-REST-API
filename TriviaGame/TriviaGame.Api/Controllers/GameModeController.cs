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
    public class GameModeController : ControllerBase
    {
        private readonly IGameModeRepository _gameModeRepo;
        public GameModeController(IGameModeRepository gameModeRepo)
        {
            _gameModeRepo = gameModeRepo;
        }
        // GET: api/GameMode
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameMode>>> GetGameModes()
        {
            var items = await _gameModeRepo.GetGameModes();
            if(items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        //// GET: api/GameMode/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/GameMode
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/GameMode/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

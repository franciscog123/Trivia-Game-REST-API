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

        //GET: api/gammode/getgamemode/5
        [HttpGet]
        [Route("GetGameMode/{id}")]
        public async Task<ActionResult<string>> GetGameMode(int id)
        {
            var gameMode = await _gameModeRepo.GetGameModeById(id);
            {
                if (gameMode == null)
                {
                    return NotFound();
                }
                return Ok(gameMode);
            }
        }
    }
}

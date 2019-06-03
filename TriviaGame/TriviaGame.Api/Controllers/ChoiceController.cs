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
    public class ChoiceController : ControllerBase
    {
        public IChoiceRepository ChoiceRepo { get; set; }

        public ChoiceController(IChoiceRepository choiceRepo)
        {
            ChoiceRepo = choiceRepo;
        }

        // GET: api/Choice
        [HttpGet]
        public IEnumerable<string> GetChoices()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Choice/5
        [HttpGet("{id}", Name = "GetChoice")]
        public async Task<ActionResult<Choice>> GetChoice(int id)
        {
            if(await ChoiceRepo.GetChoiceById(id)is Choice choice)
            {
                return choice;
            }
            return NotFound();
        }

        // POST: api/Choice
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Choice choice)
        {
            try
            {
                var id = await ChoiceRepo.CreateChoice(choice);
                Choice model = await ChoiceRepo.GetChoiceById(id);
                return CreatedAtRoute("Get", new { Id = id }, model);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetQuestionChoices/{id}")]
        public async Task<ActionResult<IEnumerable<Choice>>> GetQuestionChoices(int id)
        {
            var items = await ChoiceRepo.GetChoicesByQuestionId(id);
            if(items==null)
            {
                return NotFound();
            }
            return Ok(items);
        }
    }
}

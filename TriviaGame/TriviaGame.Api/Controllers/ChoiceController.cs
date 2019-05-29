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

        // PUT: api/Choice/5
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

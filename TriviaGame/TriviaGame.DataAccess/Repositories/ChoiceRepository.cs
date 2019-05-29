using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;

namespace TriviaGame.DataAccess.Repositories
{
    public class ChoiceRepository:IChoiceRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<ChoiceRepository> _logger;

        public ChoiceRepository(TriviaGameDbContext dbContext, ILogger<ChoiceRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> CreateChoice(Library.Models.Choice choice)
        {
            if (choice is null)
            {
                throw new ArgumentNullException(nameof(choice));
            }

            _logger.LogInformation($"Adding choice");

            Entities.Choice entity = Mapper.Map(choice);
                _dbContext.Add(entity);
                await _dbContext.SaveChangesAsync();
                return choice.ChoiceId;
        }

        public async Task<Library.Models.Choice> GetChoiceById(int choiceId)
        {
            var items = _dbContext.Choice;

            var entity = await items.FirstOrDefaultAsync(x => x.ChoiceId == choiceId);
            if(entity is null)
            {
                return null;
            }
            else
            {
                return Mapper.Map(entity);
            }
        }
    }
}

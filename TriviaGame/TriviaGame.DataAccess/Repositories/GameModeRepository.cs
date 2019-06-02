using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.DataAccess.Entities;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library.Models;

namespace TriviaGame.DataAccess.Repositories
{
    public class GameModeRepository : IGameModeRepository
    {
        private readonly TriviaGameDbContext _dbContext;
        private readonly ILogger<GameModeRepository> _logger;

        public GameModeRepository(TriviaGameDbContext dbContext, ILogger<GameModeRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Library.Models.GameMode>> GetGameModes()
        {
            try
            {
                return await Task.FromResult(Mapper.Map(_dbContext.GameMode));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public async Task<string> GetGameModeById(int id)
        {
            var items = _dbContext.GameMode;
            var entity = await items.FirstOrDefaultAsync(x => x.GameModeId == id);
            if(entity is null)
            {
                return null;
            }
            else
            {
                var gameModeString= Mapper.Map(entity).GameModeString;
                return gameModeString;
            }
        }
    }
}

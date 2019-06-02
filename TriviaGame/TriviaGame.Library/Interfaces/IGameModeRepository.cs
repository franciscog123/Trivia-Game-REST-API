using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Library.Models;

namespace TriviaGame.Library.Interfaces
{
    public interface IGameModeRepository
    {
        Task<IEnumerable<GameMode>> GetGameModes();
        Task<string> GetGameModeById(int id);

    }
}

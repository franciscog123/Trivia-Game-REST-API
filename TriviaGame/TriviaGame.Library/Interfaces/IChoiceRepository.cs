using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Library.Models;

namespace TriviaGame.Library.Interfaces
{
    public interface IChoiceRepository
    {
        Task <int> CreateChoice(Library.Models.Choice choice);
        Task<Choice> GetChoiceById(int id);
        Task<IEnumerable<Library.Models.Choice>> GetChoicesByQuestionId(int id);
    }
}

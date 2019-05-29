using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaGame.Library.Models;


namespace TriviaGame.Library.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Library.Models.Question>> GetQuestions();
        Task <Question> GetQuestionById(int questionId);
        IEnumerable<Library.Models.Question> GetQuestionsByCategoryId(int catId);
        Task<int> CreateQuestion(Library.Models.Question question);
        Task<bool> DeleteQuestion(int id);
        Task<int> GetLastQuestionAdded();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.Library.Interfaces
{
    public interface IQuizQuestionRepository
    {
        Task<Library.Models.QuizQuestion> GetQuizQuestionById(int id);
        Task<int> CreateQuizQuestion(Library.Models.QuizQuestion quizQuestion);
    }
}

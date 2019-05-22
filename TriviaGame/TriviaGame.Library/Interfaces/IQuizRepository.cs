using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.Library.Models;

namespace TriviaGame.Library.Interfaces
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> GetQuizzes(string search = null);
        Quiz GetQuizById(int id);
        void AddQuiz(Quiz quiz);
        void DeleteQuiz(int id);
        IEnumerable<Library.Models.Quiz> GetQuizzesByGameModeId(int gameModeId);
        IEnumerable<Library.Models.Quiz> GetQuizzesByUserId(int userId);
        IEnumerable<Library.Models.Quiz> GetQuizzesByCategoryId(int catId);
        int GetLastQuizId();
    }
}

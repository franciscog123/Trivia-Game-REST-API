using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.BL.Models;

namespace TriviaGame.BL.Interfaces
{
    public interface IQuizRepository
    {
        IEnumerable<Quiz> GetQuizzes(string search = null);
        Quiz GetQuizById(int id);
        void AddQuiz(Quiz quiz);
        void DeleteQuiz(int id);
        IEnumerable<BL.Models.Quiz> GetQuizzesByGameModeId(int gameModeId);
        IEnumerable<BL.Models.Quiz> GetQuizzesByUserId(int userId);
        IEnumerable<BL.Models.Quiz> GetQuizzesByCategoryId(int catId);
        int GetLastQuizId();
    }
}

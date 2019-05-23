using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.Library.Models;


namespace TriviaGame.Library.Interfaces
{
    public interface IQuestionRepository
    {
        Question GetQuestionById(int questionId);
        IEnumerable<Library.Models.Question> getQuestionsByCategoryId(int catId);
        void AddQuestion(Library.Models.Question question);
        void DeleteQuestion(int id);
    }
}

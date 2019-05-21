using System;
using System.Collections.Generic;

namespace TriviaGame.DataAccess
{
    public static class Mapper
    {
        public static Library.Models.User Map(Entities.User user) => new Library.Models.User
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            CompletedQuizzes = user.CompletedQuizzes,
            //Quizzes = Map(user.Quiz).ToList()
        };

        public static Entities.User Map(Library.Models.User user) => new Entities.User
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            CompletedQuizzes = user.CompletedQuizzes,
            //Quiz = Map(user.Quizzes).ToList()
        };

        public static Library.Models.Quiz Map(Entities.Quiz quiz) => new Library.Models.Quiz
        {
            QuizId = quiz.QuizId,
            UserId = quiz.UserId,
            CategoryId = quiz.CategoryId,
            GameModeId = quiz.GameModeId,
            Score = quiz.Score,
            Time = quiz.Time,
            //Category = Map(quiz.Category),
            //GameMode = Map(quiz.GameMode),
            User = Map(quiz.User),
           // Questions = Map(quiz.QuizQuestion).ToList
        };

        public static Entities.Quiz Map(Library.Models.Quiz quiz) => new Entities.Quiz
        {
            QuizId = quiz.QuizId,
            UserId = quiz.UserId,
            CategoryId = quiz.CategoryId,
            GameModeId = quiz.GameModeId,
            Score = quiz.Score,
            Time = quiz.Time,
            //Category = Map(quiz.Category),
            //GameMode = Map(quiz.GameMode),
            User = Map(quiz.User),
            //QuizQuestion = Map(quiz.Questions).ToList()
        };

        public static Library.Models.Question Map(Entities.Question question) => new Library.Models.Question
            {
                QuestionId = question.QuestionId,
                CategoryId = question.CategoryId,
                QuestionString = question.Question1,
                Value = question.Value,
                //Category = Map(question.Category),
                //QuizQuestions = Map(question.QuizQuestion).ToList(),
                //QuestionChoices = Map(question.Choice).ToList()
            };
    }
}

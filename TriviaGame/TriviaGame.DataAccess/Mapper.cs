using System;
using System.Collections.Generic;
using System.Linq;

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
            Quizzes = Map(user.Quiz).ToList()
        };

        public static Entities.User Map(Library.Models.User user) => new Entities.User
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            CompletedQuizzes = user.CompletedQuizzes,
            Quiz = Map(user.Quizzes).ToList()
        };

        public static Library.Models.Quiz Map(Entities.Quiz quiz) => new Library.Models.Quiz
        {
            QuizId = quiz.QuizId,
            UserId = quiz.UserId,
            CategoryId = quiz.CategoryId,
            GameModeId = quiz.GameModeId,
            Score = quiz.Score,
            Time = quiz.Time,
            Category = Map(quiz.Category),
            GameMode = Map(quiz.GameMode),
            User = Map(quiz.User),
            Questions = Map(quiz.QuizQuestion).ToList()
        };

        public static Entities.Quiz Map(Library.Models.Quiz quiz) => new Entities.Quiz
        {
            QuizId = quiz.QuizId,
            UserId = quiz.UserId,
            CategoryId = quiz.CategoryId,
            GameModeId = quiz.GameModeId,
            Score = quiz.Score,
            Time = quiz.Time,
            Category = Map(quiz.Category),
            GameMode = Map(quiz.GameMode),
            User = Map(quiz.User),
        };

        public static Library.Models.Question Map(Entities.QuizQuestion quizQuestion) =>
            Map(quizQuestion.Question);

        public static Library.Models.Question Map(Entities.Question question) => new Library.Models.Question
        {
            QuestionId = question.QuestionId,
            CategoryId = question.CategoryId,
            QuestionString = question.Question1,
            Value = question.Value,
            Category = Map(question.Category),
            QuestionChoices = Map(question.Choice).ToList()
        };

        public static Entities.Question Map(Library.Models.Question question) => new Entities.Question
        {
            QuestionId = question.QuestionId,
            CategoryId = question.CategoryId,
            Question1 = question.QuestionString,
            Value = question.Value,
            Category = Map(question.Category),
            Choice = Map(question.QuestionChoices).ToList()
        };



        public static Library.Models.Choice Map(Entities.Choice choice) => new Library.Models.Choice
        {
            ChoiceId = choice.ChoiceId,
            QuestionId = choice.QuestionId,
            Correct = choice.Correct,
            ChoiceString = choice.Choice1,
            Question = Map(choice.Question)
        };

        public static Entities.Choice Map(Library.Models.Choice choice) => new Entities.Choice
        {
            ChoiceId = choice.ChoiceId,
            QuestionId = choice.QuestionId,
            Correct = choice.Correct,
            Choice1 = choice.ChoiceString,
            Question = Map(choice.Question)
        };

        public static Library.Models.Category Map(Entities.Category category) => new Library.Models.Category
        {
            CategoryId = category.CategoryId,
            CategoryString = category.Category1,
            QuestionsByCategory = Map(category.Question).ToList(),
            QuizzesByCategory = Map(category.Quiz).ToList()
        };

        public static Entities.Category Map(Library.Models.Category category) => new Entities.Category
        {
            CategoryId = category.CategoryId,
            Category1 = category.CategoryString,
            Question = Map(category.QuestionsByCategory).ToList(),
            Quiz = Map(category.QuizzesByCategory).ToList()
        };

        public static Library.Models.GameMode Map(Entities.GameMode gameMode) => new Library.Models.GameMode
        {
            GameModeId = gameMode.GameModeId,
            GameModeString = gameMode.GameMode1,
            QuizzesByGameMode = Map(gameMode.Quiz).ToList()
        };

        public static Entities.GameMode Map(Library.Models.GameMode gameMode) => new Entities.GameMode
        {
            GameModeId = gameMode.GameModeId,
            GameMode1 = gameMode.GameModeString,
            Quiz = Map(gameMode.QuizzesByGameMode).ToList()
        };

        public static IEnumerable<Library.Models.User> Map(IEnumerable<Entities.User> users) =>
            users.Select(Map);
        public static IEnumerable<Entities.User> Map(IEnumerable<Library.Models.User> users) =>
            users.Select(Map);
        public static IEnumerable<Library.Models.Quiz> Map(IEnumerable<Entities.Quiz> quizzes) =>
            quizzes.Select(Map);
        public static IEnumerable<Entities.Quiz> Map(IEnumerable<Library.Models.Quiz> quizzes) =>
            quizzes.Select(Map);
        public static IEnumerable<Library.Models.Question> Map(IEnumerable<Entities.QuizQuestion> quizQuestions) =>
            quizQuestions.Select(Map);
        public static IEnumerable<Library.Models.Question> Map(IEnumerable<Entities.Question> questions) =>
            questions.Select(Map);
        public static IEnumerable<Entities.Question> Map(IEnumerable<Library.Models.Question> questions) =>
            questions.Select(Map);
        public static IEnumerable<Library.Models.Choice> Map(IEnumerable<Entities.Choice> choices) =>
            choices.Select(Map);
        public static IEnumerable<Entities.Choice> Map(IEnumerable<Library.Models.Choice> choices) =>
            choices.Select(Map);
        public static IEnumerable<Library.Models.Category> Map(IEnumerable<Entities.Category> categories) =>
            categories.Select(Map);
        public static IEnumerable<Entities.Category> Map(IEnumerable<Library.Models.Category> categories) =>
            categories.Select(Map);
        public static IEnumerable<Library.Models.GameMode> Map(IEnumerable<Entities.GameMode> gameModes) =>
            gameModes.Select(Map);
        public static IEnumerable<Entities.GameMode> Map(IEnumerable<Library.Models.GameMode> gameModes) =>
            gameModes.Select(Map);
    }
}

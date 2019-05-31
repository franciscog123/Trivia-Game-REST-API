using System;
using System.Collections.Generic;
using System.Text;
using TriviaGame.Api.Controllers;
using TriviaGame.Library.Interfaces;
using TriviaGame.Library.Models;
using Xunit;

namespace TriviaGame.Tests
{
    public class QuizTest
    {
        readonly Quiz quiz = new Quiz();  
        
        [Fact]
        public void Negative_ScoreThrowsException()
        {
            Assert.ThrowsAny<ArgumentException>(() => quiz.Score = -5);
        }


        [Fact]
        public void QuizQuestions_DefaultValue_Empty()
        {
            // "Empty" would throw an exception if it received a null value.
            // that would result in a failed test as expected, but this way is
            // a bit cleaner.
            Assert.NotNull(quiz.Questions);
            Assert.Empty(quiz.Questions);
        }
        [Fact]
        public void Quiz_GetSet()
        {
            //Assert.ThrowsAny<ArgumentException>(() => quiz.Score = -5);
            Quiz quiz = new Quiz();
            quiz.CategoryId = 2;
            quiz.GameModeId = 1;
            quiz.QuizId = 3;
            quiz.Score = 80;
            Assert.True(quiz.CategoryId == 2);
            Assert.True(quiz.GameModeId == 1);
            Assert.True(quiz.QuizId == 3);
            Assert.True(quiz.Score == 80);
        }



    }
}

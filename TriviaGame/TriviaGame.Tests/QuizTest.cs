using System;
using System.Collections.Generic;
using System.Text;
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
            Assert.NotNull(quiz.QuizQuestions);
            Assert.Empty(quiz.QuizQuestions);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.BL.Models
{
    public class QuizQuestion
    {
        public int QuizQuestionId { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
    }
}

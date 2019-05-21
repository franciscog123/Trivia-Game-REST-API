using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.Library.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int GameModeId { get; set; }
        public int Score { get; set; }
        public DateTime Time { get; set; }
        public List<QuizQuestion> QuizQuestions { get; set; }
        
    }
}

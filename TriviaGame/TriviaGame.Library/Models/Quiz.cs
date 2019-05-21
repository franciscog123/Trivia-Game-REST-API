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
        public Category Category { get; set; }
        public GameMode GameMode { get; set; }
        public User User { get; set; }
        public List<Question> Questions { get; set; }
        
    }
}

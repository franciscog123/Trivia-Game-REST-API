using System;
using System.Collections.Generic;

namespace TriviaGame.DataAccess.Entities
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuizQuestion = new HashSet<QuizQuestion>();
        }

        public int QuizId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int GameModeId { get; set; }
        public int Score { get; set; }
        public DateTime Time { get; set; }

        public virtual Category Category { get; set; }
        public virtual GameMode GameMode { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestion { get; set; }
    }
}

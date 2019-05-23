using System;
using System.Collections.Generic;

namespace TriviaGame.DA.Entities
{
    public partial class Question
    {
        public Question()
        {
            Choice = new HashSet<Choice>();
            QuizQuestion = new HashSet<QuizQuestion>();
        }

        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string Question1 { get; set; }
        public int Value { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Choice> Choice { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestion { get; set; }
    }
}

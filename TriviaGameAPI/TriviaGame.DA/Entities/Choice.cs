using System;
using System.Collections.Generic;

namespace TriviaGame.DA.Entities
{
    public partial class Choice
    {
        public int ChoiceId { get; set; }
        public int QuestionId { get; set; }
        public bool? Correct { get; set; }
        public string Choice1 { get; set; }

        public virtual Question Question { get; set; }
    }
}

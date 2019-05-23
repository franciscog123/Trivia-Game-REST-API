using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.BL.Models
{
    public class Choice
    {
        public int ChoiceId { get; set; }
        public int QuestionId { get; set; }
        public bool? Correct { get; set; }
        public string ChoiceString { get; set; }
        public Question Question { get; set; }
    }
}

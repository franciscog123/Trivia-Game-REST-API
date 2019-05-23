using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.BL.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string QuestionString { get; set; }
        public int Value { get; set; }
        public Category Category { get; set; }
        public List<Choice> QuestionChoices { get; set; }
    }
}

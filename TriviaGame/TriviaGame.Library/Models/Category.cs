using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.Library.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryString { get; set; }
        public List<Question> QuestionsByCategory { get; set; }
    }
}

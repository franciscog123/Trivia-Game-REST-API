using System;
using System.Collections.Generic;

namespace TriviaGame.DA.Entities
{
    public partial class Category
    {
        public Category()
        {
            Question = new HashSet<Question>();
            Quiz = new HashSet<Quiz>();
        }

        public int CategoryId { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<Question> Question { get; set; }
        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}

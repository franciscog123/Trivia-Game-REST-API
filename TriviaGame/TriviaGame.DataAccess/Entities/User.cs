using System;
using System.Collections.Generic;

namespace TriviaGame.DataAccess.Entities
{
    public partial class User
    {
        public User()
        {
            Quiz = new HashSet<Quiz>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int? CompletedQuizzes { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}

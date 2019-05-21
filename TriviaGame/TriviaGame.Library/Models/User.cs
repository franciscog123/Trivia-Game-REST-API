using System;
using System.Collections.Generic;

namespace TriviaGame.Library.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CompletedQuizzes { get; set; }
        public List<Quiz> UserQuizzes { get; set; }
    }
}

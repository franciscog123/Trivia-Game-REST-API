using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.Library.Models
{
    /// <summary>
    /// Added this class so the API would be able to send totalscore for each user along with id, name and completedquizzes.
    /// </summary>
    public class ScoreBoard
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? CompletedQuizzes { get; set; }
        public int TotalScore { get; set; }
    }
}

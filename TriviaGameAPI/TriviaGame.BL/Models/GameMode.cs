using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaGame.BL.Models
{
    public class GameMode
    {
        public int GameModeId { get; set; }
        public string GameModeString { get; set; }
        public List<Quiz> QuizzesByGameMode { get; set; }
    }
}

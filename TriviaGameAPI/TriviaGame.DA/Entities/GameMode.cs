using System;
using System.Collections.Generic;

namespace TriviaGame.DA.Entities
{
    public partial class GameMode
    {
        public GameMode()
        {
            Quiz = new HashSet<Quiz>();
        }

        public int GameModeId { get; set; }
        public string GameMode1 { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}

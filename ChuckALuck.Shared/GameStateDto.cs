using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckALuck.Shared
{
    public class GameStateDto
    {
        public string GameId { get; set; } = string.Empty;
        public RoundState State { get; set; }
        public int[] Dice { get; set; } = [];
        public List<PlayerResultDto> Results { get; set; } = new();
        public int PlayerCount { get; set; }

    }
}

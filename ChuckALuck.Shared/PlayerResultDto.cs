using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckALuck.Shared
{
    public class PlayerResultDto
    {
        public string PlayerId { get; set; } = string.Empty;
        public int Matches { get; set; }
        public int WinAmount { get; set; }
        public int TotalScore { get; set; }
    }
}

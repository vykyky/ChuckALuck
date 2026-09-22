using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckALuck.Shared
{
    public class BetDto
    {
        public string PlayerId { get; set; } = string.Empty;
        public int Number { get; set; } 
        public int Amount { get; set; }
    }
}

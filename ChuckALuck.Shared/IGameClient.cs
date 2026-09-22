using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuckALuck.Shared
{
    public interface IGameClient
    {
        Task GameStateUpdated(GameStateDto state);
        Task Error(string message);
    }
}

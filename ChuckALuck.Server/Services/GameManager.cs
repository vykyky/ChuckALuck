using ChuckALuck.Server.GameLogic;

namespace ChuckALuck.Server.Services
{
    public class GameManager
    {
        private readonly Dictionary<string, ChuckALuckGame> _games = new();

        public ChuckALuckGame GetOrCreateGame(string gameId)
        {
            if (!_games.ContainsKey(gameId))
                _games[gameId] = new ChuckALuckGame(gameId);

            return _games[gameId];
        }
    }
}

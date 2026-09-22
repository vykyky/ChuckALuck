using ChuckALuck.Shared;

namespace ChuckALuck.Server.GameLogic
{
    public class ChuckALuckGame
    {
        public string GameId { get; }
        private readonly List<BetDto> _bets = new();
        private readonly HashSet<string> _players = new();
        private readonly Random _random = new();
        private readonly Dictionary<string, int> _playerScores = new();

        public ChuckALuckGame(string gameId)
        {
            GameId = gameId;
        }

        public void AddPlayer(string playerId)
        {
            _players.Add(playerId);
            if (!_playerScores.ContainsKey(playerId))
                _playerScores[playerId] = 0;
        }

        public void PlaceBet(BetDto bet)
        {
            _bets.RemoveAll(b => b.PlayerId == bet.PlayerId);
            _bets.Add(bet);
        }


      

        public GameStateDto GetGameState()
        {
            return new GameStateDto
            {
                GameId = GameId,
                State = RoundState.Waiting, 
                Dice = Array.Empty<int>(),
                Results = _bets.Select(b => new PlayerResultDto
                {
                    PlayerId = b.PlayerId,
                    Matches = 0,
                    WinAmount = 0,
                    TotalScore = _playerScores.ContainsKey(b.PlayerId) ? _playerScores[b.PlayerId] : 0
                }).ToList(),
                PlayerCount = _players.Count
            };
        }


        public GameStateDto Roll()
        {
            if (_bets.Count < _players.Count)
            {
                return null; 
            }
            var dice = new[]
            {
                _random.Next(1, 7),
                _random.Next(1, 7),
                _random.Next(1, 7)
            };

            var results = new List<PlayerResultDto>();

            foreach (var bet in _bets)
            {
                int matches = dice.Count(d => d == bet.Number);
                int win;

                if (matches == 0)
                {
                    win = -bet.Amount;
                }
                else
                {
                    win = matches * bet.Amount;
                }

                if (!_playerScores.ContainsKey(bet.PlayerId))
                    _playerScores[bet.PlayerId] = 0;

                _playerScores[bet.PlayerId] += win;

                results.Add(new PlayerResultDto
                {
                    PlayerId = bet.PlayerId,
                    Matches = matches,
                    WinAmount = win,
                    TotalScore = _playerScores[bet.PlayerId]
                });
            }

            _bets.Clear();

            return new GameStateDto
            {
                GameId = GameId,
                State = RoundState.Finished,
                Dice = dice,
                Results = results,
                PlayerCount = _players.Count
            };
        }

        public void RemovePlayer(string playerId) => _players.Remove(playerId);
        public int PlayerCount => _players.Count;
    }
}

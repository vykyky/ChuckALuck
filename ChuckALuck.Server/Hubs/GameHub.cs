using ChuckALuck.Server.Services;
using ChuckALuck.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChuckALuck.Server.Hubs
{
    public class GameHub : Hub<IGameClient>
    {
        private readonly GameManager _gameManager;

        public GameHub(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public async Task JoinGame(string gameId, string playerId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

            var game = _gameManager.GetOrCreateGame(gameId);
            game.AddPlayer(playerId);
            await Clients.Group(gameId).GameStateUpdated(game.GetGameState());
        }


        public async Task PlaceBet(string gameId, BetDto bet)
        {
            var game = _gameManager.GetOrCreateGame(gameId);
            game.PlaceBet(bet);
        }

        public async Task Roll(string gameId)
        {
            var game = _gameManager.GetOrCreateGame(gameId);
            var state = game.Roll();

            if (state == null)
            {
                await Clients.Caller.Error("Не все игроки сделали ставку!");
                return;
            }

            await Clients.Group(gameId).GameStateUpdated(state);
        }

        public async Task LeaveGame(string gameId, string playerId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, gameId);

            var game = _gameManager.GetOrCreateGame(gameId);
            game.RemovePlayer(playerId);
            await Clients.Group(gameId).GameStateUpdated(game.GetGameState());
        }


    }
}

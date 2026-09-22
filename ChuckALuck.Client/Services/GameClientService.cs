using ChuckALuck.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChuckALuck.Client.Services;

public class GameClientService
{
    private readonly HubConnection _connection;

    public event Action<GameStateDto>? GameStateChanged;
    public event Action<string>? ErrorOccurred;

    public GameClientService(NavigationManager navigation)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5262/gamehub")
            .WithAutomaticReconnect()
            .Build();

        _connection.On<GameStateDto>(
            nameof(IGameClient.GameStateUpdated),
            state => GameStateChanged?.Invoke(state)
        );

        _connection.On<string>(
            nameof(IGameClient.Error),
            message => ErrorOccurred?.Invoke(message)
        );
    }

    public async Task ConnectAsync()
    {
        if (_connection.State == HubConnectionState.Disconnected)
            await _connection.StartAsync();
    }

    public Task JoinGame(string gameId, string playerId) =>
     _connection.InvokeAsync("JoinGame", gameId, playerId);

    public Task PlaceBet(string gameId, BetDto bet) =>
        _connection.InvokeAsync("PlaceBet", gameId, bet);

    public Task Roll(string gameId) =>
        _connection.InvokeAsync("Roll", gameId);

    public Task LeaveGame(string gameId, string playerId) =>
    _connection.InvokeAsync("LeaveGame", gameId, playerId);

}

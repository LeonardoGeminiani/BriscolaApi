namespace ApiKey2.Models;

public class Game
{
    public string GameId { get; private set; }

    public Game(string gameId)
    {
        GameId = gameId;
    }
}
using FluentResults;
using FriendZie.Domain.Player;

namespace FriendZie.Domain.Session;


public class MaximumNumberOfPLayersExceededError(int maximumNumberOfPlayers) : Error($"Maximum number of players ({maximumNumberOfPlayers}) exceeded")
{
}

public record SessionErrors
{
    public static Error MaximumNumberOfPLayersExceeded(int maximumNumberOfPlayers) => new MaximumNumberOfPLayersExceededError(maximumNumberOfPlayers);
}

public record Session
{

    public static Result<SessionType> Create(PlayerType owner)
    {
        var session = new SessionType(Guid.NewGuid(), Utility.GenerateRandomCode(4), Owner: owner, Players: [owner]);

        return Result.Ok(session);
    }

    public static Result<SessionType> AddPlayer(SessionType session, PlayerType player)
    {
        if (session.Players.Count() + 1 > session.MaximumPlayers)
            return Result.Fail(SessionErrors.MaximumNumberOfPLayersExceeded(session.MaximumPlayers));

        var playerList = session.Players.Concat([player]);
        var s = session with { Players = playerList };

        return Result.Ok(s);
    }
}


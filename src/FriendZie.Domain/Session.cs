using FluentResults;

namespace FriendZie.Domain;

/*
 * Session will have a unique identifier
 * Session will have an invitation code
 * Session will have a maximum number of players
 * Session will have a minimum number of players before game can begin
 * Session will have a Player that is an Owner
 * Session Owner can invite Player
 */


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

    public static Result<SessionType> AddPlayer(SessionType session,  PlayerType player) 
    {
        if (session.Players.Count() + 1 > session.MaximumPlayers)
            return Result.Fail(SessionErrors.MaximumNumberOfPLayersExceeded(session.MaximumPlayers));

        var playerList = Enumerable.Concat(session.Players, [player]);
        var s = session with { Players = playerList };

        return Result.Ok(s);
    }  
}


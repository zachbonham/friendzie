using FluentResults;

namespace FriendZie.Domain.Player;

/*

 * Player can join a Session with invitation code
 * */


public class InvalidPlayerNameLengthError(int length) : Error($"Player name length must be between 1 and 24. Length: {length}")
{
}

public record PlayerErrors
{
    public static Error InvalidPlayerNameLength(int length) => new InvalidPlayerNameLengthError(length);
}

public record Player
{
    public static Result<PlayerType> Create(string name) => name.Length switch
    {
        >= 1 and <= 24 => Result.Ok(new PlayerType(name)),
        _ => Result.Fail(PlayerErrors.InvalidPlayerNameLength(name.Length))
    };
}


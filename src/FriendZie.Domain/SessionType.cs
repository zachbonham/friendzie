namespace FriendZie.Domain;

public record SessionType(Guid Id, string InvitationCode, PlayerType Owner, IEnumerable<PlayerType> Players, int MaximumPlayers = 8, int MinimumPlayers = 0)
{
    public Uri GameLink => new Uri($"https://api.friendzie.com/game/{InvitationCode}");

}


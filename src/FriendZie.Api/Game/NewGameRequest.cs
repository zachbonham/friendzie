namespace FriendZie.Api.Game;

public record NewGameRequest(string OwnerName) { }
public record AddPlayerRequest (string Name, string InvitationCode) { }

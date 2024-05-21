using FriendZie.Domain;

namespace FriendZie.Api.Game;

public record CreateNewGameResult(SessionType Session) { }

public record GetGameResult(SessionType Session) { }
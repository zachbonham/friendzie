using FriendZie.Domain;

namespace FriendZie.Api.Game;

public record CreateNewGameResponse(SessionType Session) { };

public record GetGameResponse(SessionType Session) { };
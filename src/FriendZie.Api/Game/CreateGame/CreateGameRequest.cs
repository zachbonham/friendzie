using MediatR;

namespace FriendZie.Api.Game.CreateGame;

public record CreateGameRequest(string OwnerName) : IRequest<CreateGameResponse> { }



using MediatR;

namespace FriendZie.Api.Game.GetGame;

public record GetGameRequest(Guid Id) : IRequest<GetGameResponse>{ }


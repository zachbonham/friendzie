using FluentResults;
using FriendZie.Domain.Session;
using MediatR;

namespace FriendZie.Api.Game.GetGame;

public record GetGameRequest(Guid Id) : IRequest<Result<SessionType>> { }


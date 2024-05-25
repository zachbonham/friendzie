using FluentResults;
using FriendZie.Domain.Session;
using MediatR;

namespace FriendZie.Api.Game.CreateGame;

public record CreateGameRequest(string OwnerName) : IRequest<Result<SessionType>> { }


